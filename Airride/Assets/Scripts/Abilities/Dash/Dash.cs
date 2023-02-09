using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
[CreateAssetMenu(fileName = "New Dash", menuName = "Ability/Dash")]
public class Dash : Ability
{
    public float dashSpeed = 10f;
    private NewPlayerMovement playerMovement;

    public override bool Activate(GameObject parent)
    {
        playerMovement = parent.gameObject.GetComponent<NewPlayerMovement>();
        if (parent.GetComponent<PlayerManager>().IsFrozen)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public override void DuringDurtion(GameObject parent)
    {
        //parent.GetComponent<NewPlayerMovement>();
        //playerMovement = parent.gameObject.GetComponent<NewPlayerMovement>();
        playerMovement.characterController.Move(playerMovement.transform.forward * dashSpeed * Time.deltaTime);
    }

    public override void Deactivate(GameObject parent)
    {
        return;
    }
}
}
