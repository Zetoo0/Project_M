using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Orb : MonoBehaviour
{
    [SerializeField] public string color;
    const string collectedAnimation = "orb_collected_anim";
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.Play(collectedAnimation);
           // GetComponent<GameSession>().pickedUpCollectible++;
            FindObjectOfType<GameSession>().AddToCollectible(color);
            gameObject.active = false;
        }
    }
}
