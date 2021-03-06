using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{

    //SPEEDS
    [Header("Speeds")]
    [SerializeField] private float baseSpeed = 10.0f;   
    [SerializeField] private float climbSpeed = 5.0f;
    [SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);     
    [SerializeField] private AudioClip swishSound;
    private float dashDistance = 50.0f;


    [SerializeField] private ParticleSystem dust;

    [Header("Audio")]

    
    //MOVEMENTs
    private Vector2 moveInput;
    public Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;
    private float gravityScaleAtStart;
    private bool isAlive = true;
    public float jumpSpeed;

    //ATTACK
    public static int damage;
    public float attackRange;
    public LayerMask enemyLayers;
    public Transform attackPoint;


    


    //ANIMATION STATES
    private string currentState;
    private const string PLAYER_IDLE = "Player_Idle";
    private const string PLAYER_RUN = "Player_Running";
    private const string PLAYER_JUMP = "Player_Jump";
    private const string PLAYER_ATTACK = "Player_Attack_Sword";
    private const string PLAYER_DEATH = "Player_Death";
    private const string PLAYER_DASH = "Player_Dash";

    //MOVEMENT BOOLEANS, DOUBLE JUMP
    private bool isAttacking;
    public bool isJumping;
    private int extraJumps;
    [SerializeField] private int extraJumpsValue;
    private int jumpCount;
    static public bool isPlayerCanMove;
    private bool isDashing;


    [Header("Damage, Potion")]
    //DAMAGE
    Damage dmg;
    static public bool isCritted;
    [SerializeField] Transform damagePopUp;
    [SerializeField] Transform popUpPosition;

    //POTION
    [SerializeField] Transform PotionPopUp;
    [SerializeField] Transform PotionPopUpPosition;
    public bool isItemPickedUp = false;





    // Start is called before the first frame update
    void Start()
    {
        SetStartReady();
       // SetCursorStateLocked();
    }




    // Update is called once per frame
    void Update()
    {
        if (!isAlive || !isPlayerCanMove) { return; }
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
        isPlayerCanMove = true;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        Time.timeScale = 1.0f;
        jumpCount = 0;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
        dmg = new Damage();//GetComponent<Damage>();
    }
    
 
    void SetCursorStateLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnTriggerEnter2D(Collider2D collison)
    {
        if (IsItemJumpPotionAndNotPickedUp(collison))
        {
            CollidedWithPotion(collison);
        }
    }

    void CollidedWithPotion(Collider2D collison)
    {

        Instantiate(PotionPopUp, PotionPopUpPosition.position, Quaternion.identity);
        isItemPickedUp = true;
        Debug.Log("POOOTION");
        GetComponent<PotionHolder>().state = PotionHolder.PotionState.pickedUp;
        collison.gameObject.SetActive(false);
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
                Attack();
            }
            StartCoroutine(AttackCompleted());
        }
    }

    void Attack()
    {
        ChangeAnimationState(PLAYER_ATTACK);
        AudioSource.PlayClipAtPoint(swishSound, Camera.main.transform.position);
        //ChangeAnimationState(PLAYER_ATTACK);//Play attack animation
        //Detect enemies whose in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)//Damage the enemy/them
        {
            damage = dmg.DamageCalculate();
            Debug.Log(damage);
            enemy.GetComponent<EnemyMovement>().TakeDamage(damage / 2);
            Debug.Log(enemy.GetComponent<EnemyMovement>().currentHealth);
            Instantiate(damagePopUp, popUpPosition.position, Quaternion.identity);
        }
    }

    /*public int DamageCalculate()
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
    }*/

    IEnumerator AttackCompleted()
    {
        if (HasPlayerHorizontalSpeed())
        {
            ChangeAnimationState(PLAYER_RUN);
            }
        else if (!HasPlayerHorizontalSpeed())
        {
            ChangeAnimationState(PLAYER_IDLE);
            
        }
        yield return new WaitForSecondsRealtime(1); 
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
        if (!isAlive || !isPlayerCanMove) { return; }
        {
            moveInput = value.Get<Vector2>();
            Run();
        }   
        
    }
    
    IEnumerator OnJump(InputValue value)
    {
        if(!isAlive || !isPlayerCanMove) { yield return new WaitForSecondsRealtime(0); }
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        if (IsPlayerCanJump(value))//!isJumping && !isAttacking 
        {
            Jump();
            JumpCompleted();
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    void Jump()
    {
        isJumping = true;
        ChangeAnimationState(PLAYER_JUMP);
        CreateDust();
        rb.velocity += new Vector2(moveInput.x, jumpSpeed);
        jumpCount++;
    }

    bool IsPlayerCanJump(InputValue value)
    {
        bool isPlayerCanJump;
        if(value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "MoveableObstacles")) && Pause.gameState == GameState.Gameplay && isPlayerCanMove || value.isPressed && jumpCount < extraJumps && Pause.gameState == GameState.Gameplay && isPlayerCanMove)
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
        if(!isAlive || !isPlayerCanMove) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * baseSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if (HasPlayerHorizontalSpeed()) //&& //!feetCollider.CompareTag("Platform"))
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
        if(!isAlive || !isPlayerCanMove) { return; }
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
        //hogyha az aktu?lis anim?ci? = a param?terrel akkor return?li
        if(currentState == newState)
        {
            return;
        }
        //beforeState = currentState;
        //anim?ci? lej?tsz?sa
        anim.Play(newState);
        //Debug.Log(anim.GetAnimatorTransitionInfo(1));


        //?t?rjuk az ?jra az aktu?lis ?llapotunkat

        currentState = newState;
    }

    /*float GetVolume()
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
    }*/
}
