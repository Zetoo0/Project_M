using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] private float baseSpeed = 10.0f;   
    [SerializeField] private float climbSpeed = 5.0f;
    [SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);     
    [SerializeField] private AudioClip swishSound;

    private bool isDashing;
    private float dashDistance = 50.0f;

    [Header("Particles")]
    [SerializeField] private ParticleSystem dust;

    [Header("Audio")]

    
    [Header("PlayerBasics")]
    private Vector2 moveInput;
    public Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;
    private float gravityScaleAtStart;
    private bool isAlive = true;
    public float jumpSpeed;

    [Header("Attack")]
    public static int damage;
    public float attackRange;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    [Header("Potion")]
    public bool isItemPickedUp = false;

    


    //ANIMATION STATES
    private string currentState;
    private const string PLAYER_IDLE = "Player_Idle";
    private const string PLAYER_RUN = "Player_Running";
    private const string PLAYER_JUMP = "Player_Jump";
    private const string PLAYER_ATTACK = "Player_Attack_Sword";
    private const string PLAYER_DEATH = "Player_Death";
    private const string PLAYER_DASH = "Player_Dash";


    private bool isAttacking;
    public bool isJumping;

    private int extraJumps;
    [SerializeField] private int extraJumpsValue;
    private int jumpCount;


    [SerializeField]Transform damagePopUp;
    [SerializeField] Transform popUpPosition;

    [Header("Damage")]
    int critDamage = 50;
    int normalDamage = 25;
    float critPercent = 50;
    int critMin = 0;
    int critMax = 100;
    System.Random rnd = new System.Random();
    static public bool isCritted;

    [SerializeField] AudioMixerGroup auMixer;
    string exposedName = "volume";



    // Start is called before the first frame update
    void Start()
    {
        SetStartReady();
       // SetCursorStateLocked();
    }




    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        //SetCursorStateLocked();
        UpdateUpdate();

    }

    void UpdateUpdate()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

   
    void SetStartReady()
    {
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        Time.timeScale = 1.0f;
        jumpCount = 0;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }
    
 
    void SetCursorStateLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnTriggerEnter2D(Collider2D collison)
    {
        if (IsItemJumpPotionAndNotPickedUp(collison))
        {
            isItemPickedUp = true;
            Debug.Log("POOOTION");
            GetComponent<PotionHolder>().state = PotionHolder.PotionState.pickedUp;
            collison.gameObject.SetActive(false);

        }
    }

    bool IsItemJumpPotionAndNotPickedUp(Collider2D collision)
    {
        bool isItemJumpPotionAndNotPickedUp;
        if(collision.tag == "JumpBoostPotion" && !isItemPickedUp)
        {
            return isItemJumpPotionAndNotPickedUp = true;
        }
        else
        {
            return isItemJumpPotionAndNotPickedUp = false;
        }
    }

    bool IsPlayerCanAttack(InputValue value)
    {
        bool isPlayerCanAttack;

        if(!isAttacking && value.isPressed && Pause.gameState == GameState.Gameplay)
        {
            return isPlayerCanAttack = true;
        }
        else
        {
            return isPlayerCanAttack = false;
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        //Instantiate(bullet, gun.position, transform.rotation);
        //AudioSource.PlayClipAtPoint(bulletSound, Camera.main.transform.position);
        if (IsPlayerCanAttack(value))
        {
            isAttacking = true;
            if (isAttacking)
            {
                ChangeAnimationState(PLAYER_ATTACK);
                AudioSource.PlayClipAtPoint(swishSound, Camera.main.transform.position, GetVolume());
                //ChangeAnimationState(PLAYER_ATTACK);//Play attack animation
                                                    //Detect enemies whose in range
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)//Damage the enemy/them
                {
                    damage = DamageCalculate();
                    Debug.Log(damage);
                    enemy.GetComponent<EnemyMovement>().TakeDamage(damage / 2); 
                    Debug.Log(enemy.GetComponent<EnemyMovement>().currentHealth);
                    Instantiate(damagePopUp, popUpPosition.position, Quaternion.identity);
                }
            }
            AttackCompleted();
        }
    }

    public int DamageCalculate()
    {


        int critResult = Random.Range(critMin, critMax); //Same as using C# own System just include the numbers rnd.Next(critMin, critMax);
        if (!IsCrit(critResult))
        {
            return normalDamage;
        }
        else
        {
            return critDamage;
        }

    }

    static bool IsCrit(int result)
    {
        bool isCrit;
        int critPercent = 50;

        if (CritChanceValue())
        {
            if (result < critPercent)
            {
                ShowDPS.isCritted = false;
                return isCrit = false;

            }
            else
            {
                ShowDPS.isCritted = true;
                return isCrit = true;
            }
        }
        else
        {
            if (result > critPercent)
            {
                ShowDPS.isCritted = false;
                return isCrit = false;

            }
            else
            {
                ShowDPS.isCritted = true;
                return isCrit = true;
            }
        }


    }

    static bool CritChanceValue()
    {
        bool itShouldBeLittle;
        int littleOrBiggerNum = Random.Range(1, 2);
        if (littleOrBiggerNum == 1)
        {
            return itShouldBeLittle = true;
        }
        else
        {
            return itShouldBeLittle = false;
        }


        
    }

    void AttackCompleted()
    {
        if (HasPlayerHorizontalSpeed())
        {
            ChangeAnimationState(PLAYER_RUN);
            }
        else if (!HasPlayerHorizontalSpeed())
        {
            ChangeAnimationState(PLAYER_IDLE);
            
        }
        isAttacking = false;
    }



    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        }
    }


    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        {
            moveInput = value.Get<Vector2>();
            Run();
        }
        
    }
    
    IEnumerator OnJump(InputValue value)
    {
        if(!isAlive) { yield return new WaitForSecondsRealtime(0); }
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        if (IsPlayerCanJump(value))//!isJumping && !isAttacking 
        {

            isJumping = true;
            ChangeAnimationState(PLAYER_JUMP);
            CreateDust();
            rb.velocity += new Vector2(moveInput.x, jumpSpeed);
            jumpCount++;
            JumpCompleted();
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    bool IsPlayerCanJump(InputValue value)
    {
        bool isPlayerCanJump;
        if(value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "MoveableObstacles")) && Pause.gameState == GameState.Gameplay || value.isPressed && jumpCount < extraJumps && Pause.gameState == GameState.Gameplay)
        {
            return isPlayerCanJump = true;
        }
        else
        {
            return isPlayerCanJump = false;
        }
    }

   

    bool HasPlayerHorizontalSpeed()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        return playerHasHorizontalSpeed;

    }
        

    void JumpCompleted()
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "MoveableObstacles")))
        {
            if (HasPlayerHorizontalSpeed())
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else if (!HasPlayerHorizontalSpeed())
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
            isJumping = false;
            jumpCount = 0;
           
        }


        //ChangeAnimationState(beforeState);
    }

    void OnDash(InputValue value)
    {
        if(!isAlive) { return;  }
        if (value.isPressed && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
   

    IEnumerator Dash()
    {
        if(!isAlive) { yield return new WaitForSecondsRealtime(0); }
        Debug.Log("Dashed");
        ChangeAnimationState(PLAYER_DASH);
        isDashing = true;
        rb.velocity += new Vector2(dashDistance * moveInput.x, 0f);
        rb.AddForce(new Vector2(dashDistance * moveInput.x, 0f));
    
        yield return new WaitForSeconds(1.0f);
        isDashing = false;
    }

    void Run()
    {
        if(!isAlive) { return; }
       // bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * baseSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if (HasPlayerHorizontalSpeed() && !feetCollider.CompareTag("Platform"))
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else if (!HasPlayerHorizontalSpeed())
        {
            ChangeAnimationState(PLAYER_IDLE);
        }


    }

    public void FlipSprite()
    {
       // bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
       
        if (HasPlayerHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1.0f);
            
        }

    }

    void ClimbLadder()
    {
        if(!isAlive) { return; }
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            return;
        }
        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0.0f;
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        

    }

    void Die()
    {
        if(!isAlive) { return; }  
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards","Trap")))
        {
            ProcessDeath();
        }
    }

    void ProcessDeath()
    {
        isAlive = false;
        ChangeAnimationState(PLAYER_DEATH);
        rb.velocity = deathKick;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    public void ChangeAnimationState(string newState)
    {
        //hogyha az aktuális animáció = a paraméterrel akkor returnöli
        if(currentState == newState)
        {
            return;
        }
        //beforeState = currentState;
        //animáció lejátszása
        anim.Play(newState);
        //Debug.Log(anim.GetAnimatorTransitionInfo(1));


        //Átírjuk az újra az aktuális állapotunkat

        currentState = newState;
    }

    float GetVolume()
    {
        float volOut;
        bool isCanGetVol = auMixer.audioMixer.GetFloat(exposedName, out volOut); //auMixer.GetFloat(exposedName, out volOut);
        if (isCanGetVol)
        {
            return volOut;
        }
        else
        {
            return 0f;
        }

    }


}
