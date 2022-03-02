using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerIsInSpikeRange : MonoBehaviour
{

    BoxCollider2D collider;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckCollisionTag(collision))
        {
            GetComponentInChildren<spikeTrap>().isEdgeColliderTouched = true;
        }
    }
    bool CheckCollisionTag(Collider2D collision)
    {
        bool isPlayerTag;

        if (collision.tag == "Player")
        {
            return isPlayerTag = true;
        }
        else
        {
            return isPlayerTag = false;
        }
    }

}
