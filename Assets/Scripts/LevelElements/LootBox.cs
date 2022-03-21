
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


    //Chest states
    enum Chest
    {
        open,
        close
    };

    Chest chestState = Chest.close;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player" && !wasCollected)
        {
            switch (chestState)
            {
                case Chest.close://Chect kinyitása, ha zárva van
                    ChangeAnimationState(LOOTBOX_OPEN);
                    potion.SetActive(true);
                    wasCollected = true;
                    break;
                case Chest.open:
                    return;
            }


            
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
