using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    

    [Header("Objects")]
    [SerializeField] float baseSpeed = 10.0f;
    [SerializeField] public float baseJumpSpeed = 10.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    //[SerializeField] GameObject bullet;
    //[SerializeField] Transform gun;
    //[SerializeField] AudioClip bulletSound;
    [SerializeField] AudioClip swishSound;

    [Header("Dash")]
    bool isDashing;
    float dashDistance = 30.0f;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    float direction;

    [Header("Particles")]
    public ParticleSystem dust;


    
    [Header("PlayerBasics")]
    Vector2 moveInput;
    public Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    //int deaths = 0;
    public float jumpSpeed;

    [Header("Attack")]
    public int damage = 20;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    [Header("Potion")]
    public bool isPickedUp = false;
    public float activeTime = 5f;

    [Header("CheckPoint")]
    private Vector3 respawnPoint;

    PotionHolder potion;

    //ANIMATION STATES
    string currentState;
    const string PLAYER_IDLE = "Metroid_Idle";
    const string PLAYER_RUN = "MetroidVania_Running";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_ATTACK = "Player_Attack_Sword";
    const string PLAYER_DEATH = "MetroidVania_Death";
    const string PLAYER_DASH = "Player_Dash";

    //ANIMATION TRIGGERS AND BOOLS
    /*const string deathAnimationTrigger = "Dying";
    const string jumpAnimationTrigger = "Jumping";
    const string attackAnimationTrigger = "Attack";
    const string runningAnimationBool = "IsRunning";
    const string isAttackCompletedBool = "IsAttackCompleted";
    const string isJumpingAnimationBool = "IsJumping";
    const string isGroundedAnimatinBool = "IsGrounded";*/

    bool isAttacking;
    bool isJumping;
    string beforeState;
    bool isRunning = false;
    public bool canDoubleJump = false;

    int extraJumps;
    public int extraJumpsValue;
    int jumpCount;
    bool canJump;
    bool isGrounded;


    


    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 0;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
        dashTime = startDashTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        //SetCursorStateLocked();
        Run();
        FlipSprite();
        ClimbLadder();
        Die();

        //direction = transform.localScale.x; 
    }

    void OnNavigation(InputValue value)
    {
        value.Get<Vector2>();
    }
 
    void SetCursorStateLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.tag == "JumpBoostPotion" && !isPickedUp)
        {
            isPickedUp = true;
            Debug.Log("POOOTION");
            //jumpSpeed = 20f;
            GetComponent<PotionHolder>().state = PotionHolder.PotionState.pickedUp;
            Destroy(collison.gameObject);
            //yield return new WaitForSecondsRealtime(activeTime);
            //jumpSpeed = baseJumpSpeed;
            //Debug.Log("Inactive");
            //isPickedUp = false;
        }
        /*else if(collison.tag == "Stone")
        {
            collison.attachedRigidbody.velocity = new Vector2(10f, 0f);
        }*/
        
       
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        //Instantiate(bullet, gun.position, transform.rotation);
        //AudioSource.PlayClipAtPoint(bulletSound, Camera.main.transform.position);
        if (!isAttacking && !isJumping && value.isPressed)
        {
            isAttacking = true;
            if (isAttacking)
            {
                ChangeAnimationState(PLAYER_ATTACK);
                //ChangeAnimationState(PLAYER_ATTACK);//Play attack animation
                                                    //Detect enemies whose in range
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)//Damage the enemy/them
                {
                    enemy.GetComponent<EnemyMovement>().TakeDamage(damage);
                }
            }

            
        }
        AttackCompleted();


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
        
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground","MoveableObstacles")) || value.isPressed && jumpCount < extraJumps)//!isJumping && !isAttacking 
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
            StartCoroutine(Dash(direction));
        }
        
            
        
        
    }

    void CreateDust()
    {
        dust.Play();
    }
   

    IEnumerator Dash(float direction)
    {
        Debug.Log("Dashed");
        ChangeAnimationState(PLAYER_DASH);
        isDashing = true;
        rb.velocity += new Vector2(dashDistance * moveInput.x, 0f);
        rb.AddForce(new Vector2(dashDistance * moveInput.x, 0f));




        
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
    }

    void Run()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
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
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1.0f);
            
        }

    }

    void ClimbLadder()
    {
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
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards","Trap")))
        {
            isAlive = false;
            ChangeAnimationState(PLAYER_DEATH);
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
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
  



}
