using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Double Jump Potion")]
public class DoubleJumpPotion : Potion
{
    public override void Activate(GameObject parent)
    {
        PlayerMovement player = parent.GetComponent<PlayerMovement>();
        player.canDoubleJump = true;

    }
}
