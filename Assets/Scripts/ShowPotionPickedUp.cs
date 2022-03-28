
using UnityEngine;
using TMPro;

public class ShowPotionPickedUp : MonoBehaviour
{

    TextMeshPro PotionTextPos;
    Animator anim;
    private string PotionAnimName = "PotinAnim";
    private string PotionText = "Jump Potion Picked Up";


    private float disappearTime = 1.0f;
    private float stopper = 0.0f;

    void Start()
    {
        PotionTextPos = GetComponent<TextMeshPro>();
        PotionTextPos.SetText(PotionText);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PopUp();
    }

    void PopUp()
    {
        if (stopper < disappearTime)
        {

            stopper += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
