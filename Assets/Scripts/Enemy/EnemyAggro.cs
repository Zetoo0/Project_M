using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [SerializeField]
    public Transform player;        

    [SerializeField]
    private float agroRange;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform castPoint;


    private Rigidbody2D rb;

    public bool isFacingLeft;
    bool isSearching;
    private bool isAgro = false;

    const string ENEMY_RUN = "Enemy_Run";

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        EnemyAggroState();
        CheckFaceing();
    }

    void EnemyAggroState()
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


        /*if (isAgro)
        {
            isSearching = true;
            //if (!isSearching)
            //{
                //isSearching = true;
                    //StopChasingPlayer();
            Invoke("StopChasingPlayer", 2);
                    //StartCoroutine(StopChasingPlayer());
                }
            }
            //Stop chasing the player
            //StopChasingPlayer();
            //rb.GetComponent<EnemyMovement>().ChangeAnimationState("Enemy_Idle");
        */


        if (isAgro)
        {
            ChasePlayer();
            Invoke("StopChasingPlayer", 4);
            GetComponent<EnemyMovement>().FlipEnemyFaceing();
        }

    }

    void CheckFaceing()
    {
        if (transform.localScale.x == -1)
        {
            isFacingLeft = true;
        }
        else
        {
            isFacingLeft = false;
        }
    }


    void ChasePlayer()//A player után futás, checkeli, hogy melyik oldalt található a player
    {

        if (player.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            //enemy is the left side and the player is on the left so move right
            GetComponent<EnemyMovement>().FlipEnemyFaceing();
            GetComponent<EnemyMovement>().ChangeAnimationState(ENEMY_RUN);
            isFacingLeft = false;
        }
        else if (player.position.x < transform.position.x)
        {
            //enemy is the right side and the player is on the left so move right
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
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

    //Látja-e az ellenség a Playert, RayCasttal megoldva
    bool CanSeePlayer(float distance)
    {
        bool value = false;
        var castDistance = -distance;

        if (isFacingLeft)
        {
            castDistance = distance;
        }


        Vector2 endPosition = castPoint.position + Vector3.right * castDistance ;//== new Vector3(position.x * distance)
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