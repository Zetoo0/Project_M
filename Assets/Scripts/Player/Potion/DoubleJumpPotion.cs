
using UnityEngine;

public class DoubleJumpPotion : Potion
{
    public override void Activate(GameObject parent)
    {
        PlayerMovement player = parent.GetComponent<PlayerMovement>();
        //player.canDoubleJump = true;

    }
}
