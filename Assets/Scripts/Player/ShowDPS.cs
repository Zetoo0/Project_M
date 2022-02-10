using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDPS : MonoBehaviour
{

    //[SerializeField]Transform damagePosition;
    [SerializeField]TextMeshPro damageTextPos;

    void Start()
    {
        //damageTextPos = GetComponentInChildren<TextMeshProUGUI>();
        damageTextPos.text = "";
    }

    public IEnumerator ShowDamage(int damage)
    {
        damageTextPos.text = damage.ToString();

        Debug.Log("Damaged: " + damage);

        yield return new WaitForSecondsRealtime(1.0f);

        damageTextPos.SetText("");

       // damageTextPos.enabled = false;

    }

}
