using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Jump Potion")]
public class JumpPotion : Potion
{
    public float jumpspeed;

    public override void Activate(GameObject parent)
    {
        PlayerMovement player = parent.GetComponent<PlayerMovement>();
        player.jumpSpeed = jumpspeed;
        Debug.Log("JumpSpeed set to " + jumpspeed);
        
    }

}
