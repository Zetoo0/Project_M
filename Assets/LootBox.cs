using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    bool wasCollected;
    Animator anim;
    string currentState;
    [SerializeField] GameObject potion;
    [SerializeField] Transform dropPosition;
    GameObject[] objects;

    //ANIMATIONS
    const string LOOTBOX_OPEN = "lootbox_open_animation";

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
            //anim.SetTrigger("Opened");
            ChangeAnimationState(LOOTBOX_OPEN);
            potion.SetActive(true);
            //Debug.Log("Drop hopp!");
            wasCollected = true;
        }
    }

    

    void ChangeAnimationState(string newState)
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
