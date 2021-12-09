using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            bool left = Mathf.Abs(collision.rigidbody.velocity.x) > Mathf.Epsilon;
            if (left)
            {
                rb.velocity = new Vector2(-speed, 0f);

            }
            else
            {
                rb.velocity = new Vector2(speed, 0f);

            }

        }

    }



}

