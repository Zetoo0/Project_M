using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrap : MonoBehaviour
{

    BoxCollider2D touchCollider;
    public bool isEdgeColliderTouched;
    [SerializeField]Rigidbody2D spikeRb;
    float fallingspeed = 5.0f;
    
    
    
    void Start()
    {
        touchCollider = GetComponent<BoxCollider2D>();
        spikeRb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherCollider.tag == "Platform")
        {
            this.transform.position = GetPosition();
        }    
    }

    Vector3 GetPosition()
    {
        Vector3 nowPos = new Vector3(this.transform.position.x, this.transform.position.y);

        return nowPos;
    }


    void Update()
    { 
        FallSpikes();  
    
    }

    void FallSpikes()
    {
        if (isEdgeColliderTouched)
        {
            this.transform.position += new Vector3(0.0f, -0.2f);
        }
    }
}


