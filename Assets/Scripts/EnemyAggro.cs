using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [SerializeField]
    public Transform Player;

    [SerializeField]
    public float agroRange;

    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public Transform castPoint;

    Rigidbody2D rb;

    bool isFacingLeft;
    bool isAgro = false;
    bool isSearching = false;

    const string ENEMY_RUN = "Enemy_Run";


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Distance to player
        //float distToPlayer = Vector2.Distance(transform.position, Player.position);
        if (CanSeePlayer(agroRange))
        {
            //Chase the player
            //ChasePlayer();
            isAgro = true;
            //rb.GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Run");
        }
        
        
        if (isAgro)
        {
            isSearching = true;
            if (!isSearching)
            {
                isSearching = true;
                    //StopChasingPlayer();
                Invoke("StopChasingPlayer", 2);
                    //StartCoroutine(StopChasingPlayer());
                }
            }
            //Stop chasing the player
            //StopChasingPlayer();
            //rb.GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Idle");

        

        if (isAgro)
        {
            ChasePlayer();
        }

    }

    void ChasePlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            //enemy is the left side and the player is on the right so move right
            rb.velocity = new Vector2(moveSpeed, 0f);
            GetComponent<EnemyMovement>().FlipEnemyFaceing();
            GetComponent<EnemyMovement>().ChangeAnimationState(ENEMY_RUN);
            isFacingLeft = false;
        }
        else
        {
            //enemy is the right side and the player is on the left so move left
            rb.velocity = new Vector2(-moveSpeed, 0f);
            GetComponent<EnemyMovement>().FlipEnemyFaceing();
            GetComponent<EnemyMovement>().ChangeAnimationState(ENEMY_RUN);
            isFacingLeft = true;
        }
    }

    void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        rb.velocity = new Vector2(0f, 0f);
        GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Idle");
        //yield return new WaitForSecondsRealtime(5); 
    }

    bool CanSeePlayer(float distance)
    {
        bool value = false;
        var castDistance = distance;

        if (isFacingLeft)
        {
            castDistance = -distance;
        }


        Vector2 endPosition = castPoint.position + Vector3.right * castDistance;//== new Vector3(position.x * distance)
        RaycastHit2D rcHit = Physics2D.Linecast(castPoint.position, endPosition,LayerMask.GetMask("MoveableObstacle","Player","Ground"));

        if (rcHit.collider != null)
        {
            if (rcHit.collider.gameObject.CompareTag("Player"))
            {
                value = true;
            }
            else
            {
                value = false;
            }
            Debug.DrawLine(castPoint.position, rcHit.point, Color.green);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPosition, Color.blue); ;
        }

        return value;
    }


}