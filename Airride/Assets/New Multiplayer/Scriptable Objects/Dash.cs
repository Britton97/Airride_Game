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

    public override void Activate(GameObject parent)
    {
        playerMovement = parent.gameObject.GetComponent<NewPlayerMovement>();
    }
    public override void DuringDurtion(GameObject parent)
    {
        //parent.GetComponent<NewPlayerMovement>();
        //playerMovement = parent.gameObject.GetComponent<NewPlayerMovement>();
        playerMovement.characterController.Move(playerMovement.transform.forward * dashSpeed * Time.deltaTime);
    }
}
}
