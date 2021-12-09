using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    Rigidbody2D rb;
    CapsuleCollider2D bodyCollider;

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
    }

    void Update()
    {
        Hit();    
    }

    void Hit()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            ChangeAnimationState(ENEMY_ATTACK);
            ChangeAnimationState(ENEMY_IDLE);
        }
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

        //hogyha az aktu�lis anim�ci� = a param�terrel akkor return�li
        if (currentState == newState)
        {
            return;
        }

        //anim�ci� lej�tsz�sa
        anim.Play(newState);
        

        //�t�rjuk az �jra a mostani �llapotunkat
        currentState = newState;
    }

}
    