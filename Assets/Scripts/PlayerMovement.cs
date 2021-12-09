using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] float baseSpeed = 10.0f;
    [SerializeField] public float baseJumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] AudioClip bulletSound;

    [Header("Dash")]
    bool isDashing;
    float dashDistance = 15.0f;

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
    public float jumpSpeed = 5.0f;

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

    //ANIMATION STATES
    string currentState;
    const string PLAYER_IDLE = "Metroid_Idle";
    const string PLAYER_RUN = "MetroidVania_Running";
    const string PLAYER_JUMP = "MetroidV_JumpUp";
    const string PLAYER_ATTACK = "Player_MetroidV_Attack_Sword";
    const string PLAYER_DEATH = "MetroidVania_Death";



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    IEnumerator OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.tag == "JumpBoostPotion" && !isPickedUp)
        {
            isPickedUp = true;
            Debug.Log("POOOTION");
            jumpSpeed = 20f;
            Destroy(collison.gameObject);
            yield return new WaitForSecondsRealtime(activeTime);
            jumpSpeed = baseJumpSpeed;
            Debug.Log("Inactive");
            isPickedUp = false;
        }
        else if(collison.tag == "Stone")
        {
            collison.attachedRigidbody.velocity = new Vector2(10f, 0f);
        }
        
       
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        //Instantiate(bullet, gun.position, transform.rotation);
        //AudioSource.PlayClipAtPoint(bulletSound, Camera.main.transform.position);
        ChangeAnimationState(PLAYER_ATTACK);//Play attack animation
        ChangeAnimationState(PLAYER_IDLE);
        //Detect enemies whose in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)//Damage the enemy/them
        {
            enemy.GetComponent<EnemyMovement>().TakeDamage(damage);
        }


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
            Debug.Log(moveInput);
        }
        
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground","MoveableObstacle")))
        {
            ChangeAnimationState(PLAYER_JUMP);
            CreateDust();
            rb.velocity += new Vector2(0f, jumpSpeed);
            ChangeAnimationState(PLAYER_IDLE);
        }
        
    }

    void OnDash(InputValue value)
    {
        if(!isAlive) { return;  }
        if (value.isPressed && !isDashing)
        {
            //dash animation
            //rb.velocity = new Vector2(dashDistance * moveInput.x,0f);  
            StartCoroutine(Dash(1));
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
        rb.velocity += new Vector2(dashDistance * direction, rb.velocity.y);
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
    }

    void Run()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * baseSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if (playerHasHorizontalSpeed)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
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
            ChangeAnimationState(PLAYER_DEATH);
            //anim.SetTrigger("Dying");
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void ChangeAnimationState(string newState)
    {

        //hogyha az aktuális animáció = a paraméterrel akkor returnöli
        if(currentState == newState)
        {
            return;
        }

        //animáció lejátszása
        anim.Play(newState);

        //Átírjuk az újra az aktuális állapotunkat
        currentState = newState;
    }
  



}
