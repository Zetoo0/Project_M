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
    public int maxHealth = 100;
    int currentHealth;
    Animator anim;
    string currentState;

    //ANIMATION STATES
    public const string ENEMY_IDLE = "Enemy_Idle";
    public const string ENEMY_RUN = "Enemy_Run";
    public const string ENEMY_HIT = "Enemy_Hit";
    public const string ENEMY_DEATH = "Enemy_Death";
    public const string ENEMY_ATTACK = "Enemy_Attack";


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        wallCheck = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Hit();
        StartCoroutine(FlipEnemyFaceing());
        CheckWall();
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

    


    void OnTriggerExit2D(Collider2D other)
    {
            moveSpeed = -moveSpeed;
            FlipEnemyFaceing();
        
        
    }

    public IEnumerator FlipEnemyFaceing()
    {
        yield return new WaitForSecondsRealtime(5);
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1.0f);
        if(transform.localScale.x == -1)
        {
            GetComponent<EnemyAggro>().isFacingLeft = false;
        }
        else
        {
            GetComponent<EnemyAggro>().isFacingLeft = true;
        }

    }

 

    public void ChangeAnimationState(string newState)
    {

        //hogyha az aktuális animáció = a paraméterrel akkor returnöli
        if (currentState == newState)
        {
            return;
        }

        anim.SetFloat("velocityY",rb.velocity.y);
        anim.SetFloat("velocityX", rb.velocity.x);

        //animáció lejátszása
        anim.Play(newState);
        

        //Átírjuk az újra a mostani állapotunkat
        currentState = newState;
    }

}
    