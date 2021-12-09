using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{

    [SerializeField]
    public Transform castPoint;

    [SerializeField]
    public Transform Player;
    
    [SerializeField]
    public float agroRange;

    [SerializeField]
    public float moveSpeed;

    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Distance to player
        float distToPlayer = Vector2.Distance(transform.position, Player.position);
        if(distToPlayer < agroRange)
        {
            //Chase the player
            ChasePlayer();
            //rb.GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Run");
        }
        else
        {
            //Stop chasing the player
            StopChasingPlayer();
            //rb.GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Idle");
        }
        
    }

    void ChasePlayer()
    {
        if(transform.position.x < Player.position.x)
        {
            //enemy is the left side and the player is on the right so move right
            rb.velocity = new Vector2(moveSpeed, 0f);
            GetComponent<EnemyMovement>().FlipEnemyFaceing();
            GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Run");
        }
        else
        {
            //enemy is the right side and the player is on the left so move left
            rb.velocity = new Vector2(-moveSpeed, 0f);
            GetComponent<EnemyMovement>().FlipEnemyFaceing();
            GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Run");
        }
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0f,0f);
        GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Idle");
    }

    bool CanSeePlayer(float distance)
    {
        bool value = false;
        var castDistance = distance;
        Vector2 endPosition = castPoint.position + Vector3.right * distance;//== new Vector3(position.x * distance)
        RaycastHit2D rcHit = Physics2D.Linecast(castPoint.position,endPosition, 1 << LayerMask.NameToLayer("Action"));

        if(rcHit.collider != null)
        {
            if (rcHit.collider.gameObject.CompareTag("Player"))
            {
                value = true;
            }
            else
            {
                value = false;
            }
        }
        Debug.DrawLine(castPoint.position, rcHit.point, Color.blue);

        return value;
    }

}
