using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    Rigidbody2D rb;
    CapsuleCollider2D bodyCollider;
    CircleCollider2D wallCheck;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    public int currentHealth;
    Animator anim;
    private string currentState;

    //ANIMATION STATES
    public const string ENEMY_IDLE = "Enemy_Idle";
    public const string ENEMY_RUN = "Enemy_Run";
    public const string ENEMY_HIT = "Enemy_Hit";
    public const string ENEMY_DEATH = "Enemy_Death";
    public const string ENEMY_ATTACK = "Enemy_Attack";


    void Start()
    {
        SetStart();
    }

    void Update()
    {
        UpdateUpdate();
    }

    void UpdateUpdate()
    {
        Hit();
        //InvokeRepeating("FlipEnemyFaceing", 5f,100);
        CheckWall();
    }

    void SetStart()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        wallCheck = GetComponent<CircleCollider2D>();
    }


    void CheckWall()
    {
        if (wallCheck.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            FlipEnemyFaceing();
        }
    }

    void Hit()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            ChangeAnimationState(ENEMY_ATTACK);
            //ChangeAnimationState(ENEMY_IDLE);
        }
    }

    void Die()
    {
        //anim.SetBool("IsDead", true);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        ChangeAnimationState(ENEMY_HIT);
        //anim.SetTrigger("Hit");
        //ChangeAnimationState(ENEMY_IDLE);
        if(currentHealth <= 0)
        {
            ChangeAnimationState(ENEMY_DEATH);
            Die();
        }
        else
        {
            ChangeAnimationState(ENEMY_IDLE);
        }
    }

    


    /*void OnTriggerExit2D(Collider2D other)
    {
            moveSpeed = -moveSpeed;
            FlipEnemyFaceing();
        
        
    }*/

    public void FlipEnemyFaceing()
    {
        Debug.Log("Flip");
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1.0f);
        if(transform.localScale.x == -1)
        {
            GetComponent<EnemyAggro>().isFacingLeft = true;
        }
        else if(transform.localScale.x == 1)
        {
            GetComponent<EnemyAggro>().isFacingLeft = false;
        }

    }

 

    public void ChangeAnimationState(string newState)
    {

        //hogyha az aktuális animáció = a paraméterrel akkor returnöli
        if (currentState == newState)
        {
            return;
        }


        //animáció lejátszása
        anim.Play(newState);
        

        //Átírjuk az újra a mostani állapotunkat
        currentState = newState;
    }

}
    