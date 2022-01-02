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
    float dashDistance = 15.0f;
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
    /*string currentState;
    const string PLAYER_IDLE = "Metroid_Idle";
    const string PLAYER_RUN = "MetroidVania_Running";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_ATTACK = "Player_Attack_Sword";
    const string PLAYER_DEATH = "MetroidVania_Death";*/

    //ANIMATION TRIGGERS AND BOOLS
    const string deathAnimationTrigger = "Dying";
    const string jumpAnimationTrigger = "Jumping";
    const string attackAnimationTrigger = "Attack";
    const string runningAnimationBool = "IsRunning";


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
        anim.SetBool("IsRunning", false);

        //ChangeAnimationState(PLAYER_IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        //direction = transform.localScale.x; 
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
        if (!isAttacking && !isJumping)
        {
            isAttacking = true;
            if (isAttacking)
            {
                anim.SetTrigger(attackAnimationTrigger);
                //ChangeAnimationState(PLAYER_ATTACK);//Play attack animation
                                                    //Detect enemies whose in range
                AudioSource.PlayClipAtPoint(swishSound, Camera.main.transform.position);
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
        isAttacking = false;
        anim.SetBool(runningAnimationBool, true);
        //ChangeAnimationState(PLAYER_IDLE);
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
            //Debug.Log(moveInput);
            Run();
        }
        
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground","MoveableObstacles")) || value.isPressed && jumpCount < extraJumps)//!isJumping && !isAttacking 
        {
            isJumping = true;
            anim.SetTrigger("Jumping");
            //ChangeAnimationState(PLAYER_JUMP);
            CreateDust();
            rb.velocity += new Vector2(moveInput.x, jumpSpeed);
            jumpCount++;
            //JumpCountCheck();
            JumpCompleted();


        }

    }

    

    void JumpCountCheck()
    {
        
    }



    void JumpCompleted()
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "MoveableObstacles")))
        {
            anim.SetBool(runningAnimationBool, true);
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
            //dash animation
            //rb.velocity = new Vector2(dashDistance * moveInput.x,0f);  
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
        isDashing = true;
        //rb.velocity += new Vector2(dashDistance * direction, 0f);
        //rb.AddForce(new Vector2(dashDistance * direction, 0f));
        rb.velocity += new Vector2(dashDistance * moveInput.x, 0f);
        rb.AddForce(new Vector2(dashDistance * moveInput.x, 0f));




        /*if (isDashing)
        {
            if(direction == -1)
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
            else
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
        }*/
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
    }

    void Run()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * baseSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        anim.SetBool(runningAnimationBool, playerHasHorizontalSpeed);
        //anim.SetBool("IsRunning", playerHasHorizontalSpeed);


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
            //anim.SetBool("IsClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0.0f;
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        
        //anim.SetBool("IsClimbing", playerHasVerticalSpeed);

    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards","Trap")))
        {
            isAlive = false;
            anim.SetTrigger(deathAnimationTrigger);
            //ChangeAnimationState(PLAYER_DEATH);
            //anim.SetTrigger("Dying");
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    /*public void ChangeAnimationState(string newState)
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
        
        
        
    }*/
  



}
