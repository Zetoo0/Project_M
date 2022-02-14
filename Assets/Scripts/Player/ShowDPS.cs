using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDPS : MonoBehaviour
{

    //[SerializeField]Transform damagePosition;
    TextMeshPro damageTextPos;
    Animator anim;
    private string nonCritAnimationName = "DmgTextFloat";
    private string critAnimationName = "CritDamageTextFloat";
    string currentState;

    private float disappearTime = 1.0f;
    private float stopper = 0.0f;

    public static bool isCritted;

    void Awake()
    {
        //stopper = 0.0f;
        //PopUp();
    }

    private void Start()
    {
        damageTextPos = GetComponent<TextMeshPro>();
        damageTextPos.SetText(PlayerMovement.damage.ToString());
        anim = GetComponent<Animator>();
        IsCrit();
    }

    void IsCrit()
    {
        if (isCritted)
        {
            anim.Play(critAnimationName);
        }
        else
        {
            anim.Play(nonCritAnimationName);
        }
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


   /* void PopUp()
    {
        while(stopper != disappearTime)
        { 
            stopper += Time.deltaTime;
            Debug.Log(stopper);
           // transform.position = new Vector3(transform.position.x, transform.position.y + 1) * Time.deltaTime;
        }
        if (stopper == disappearTime)
        {
            Destroy(gameObject);
        }




    }*/

    /*void Start()
    {
        anim = GetComponent<Animator>();
        //damageTextPos = GetComponentInChildren<TextMeshProUGUI>();
        damageTextPos.text = "";


    }

    
    bool CheckDamageIsNotNull(int damage)
    {
        bool isDamageNull;

        if(damage == 0)
        {
            isDamageNull = true;
        }
        else
        {
            isDamageNull = false;
        }

        return isDamageNull;
    }

    public void ShowDamage(int damage)
    {

        CheckDamageIsNotNull(damage);

        damageTextPos.text = damage.ToString();
        
        //ChangeAnimationState(animationName);

        Debug.Log("Damaged: " + damage);

        //yield return new WaitForSecondsRealtime(1.0f);

        damageTextPos.text = "";

       // damageTextPos.enabled = false;

    }

    public void ChangeAnimationState(string newState)
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
    }*/

}
