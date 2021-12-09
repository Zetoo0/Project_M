using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;

    Rigidbody2D rb;//My Rigidbody
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed,0f);
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        if(other.tag == "Platform")
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1.0f);
        }
        else
        {
            Destroy(gameObject);

        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
