using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    Rigidbody2D rb;

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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
            
    }

    void Die()
    {
        //anim.SetBool("IsDead", true);
        ChangeAnimationState(ENEMY_DEATH);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        ChangeAnimationState(ENEMY_HIT);
        //anim.SetTrigger("Hit");
        ChangeAnimationState(ENEMY_IDLE);
        if(currentHealth == 0)
        {
            Die();
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFaceing();
    }

    public void FlipEnemyFaceing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1.0f);
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
    