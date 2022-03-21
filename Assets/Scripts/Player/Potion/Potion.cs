using UnityEngine;

public class Potion : ScriptableObject
{
    public new string name;
    public float activeTime;

    public virtual void Activate(GameObject parent)
    {

    }

}
