using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    bool wasCollected = false;
    Animator anim;
    string currentState;
    [SerializeField] GameObject potion;
    [SerializeField] Transform dropPosition;
    GameObject[] objects;

    //ANIMATIONS
    const string LOOTBOX_OPEN = "lootbox_open_animation";

    enum Chest
    {
        open,
        close
    };

    Chest chestState = Chest.close;

    void Start()
    {
        anim = GetComponent<Animator>();
        //potion = GetComponent<GameObject>();
        //dropPosition = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !wasCollected)
        {
            switch (chestState)
            {
                case Chest.close:
                    ChangeAnimationState(LOOTBOX_OPEN);
                    potion.SetActive(true);
                    wasCollected = true;
                    break;
                case Chest.open:
                    return;
            }


            //anim.SetTrigger("Opened");
            /*
            ChangeAnimationState(LOOTBOX_OPEN);
            potion.SetActive(true);
            
            //Debug.Log("Drop hopp!");
            wasCollected = true;*/
        }

        
    }

    

    void ChangeAnimationState(string newState)
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
