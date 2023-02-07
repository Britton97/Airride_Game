using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
public class AbilityHolder : MonoBehaviourPunCallbacks
{
    public Ability ability;
    public float activeTime;
    public float cooldownTime;
    private bool usable = true;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    void Awake()
    {
        Ability_Button.AbilityUsed += UseTapAbility;
        //Ability_Button.AbilityDeactivated += ability.Deactivate;
    }
    
    void OnDestroy()
    {
        Ability_Button.AbilityUsed -= UseTapAbility;
    }

    #region Tap Ability
    void UseTapAbility()
    {
        if(usable && photonView.IsMine)
        {
            if(ability.Activate(gameObject)){StartCoroutine(ActiveCoroutine());}
        }
    }

    private IEnumerator ActiveCoroutine()
    {
        usable = false;
        activeTime = ability.activeTime;
        while(activeTime > 0)
        {
            ability.DuringDurtion(gameObject);
            activeTime -= Time.deltaTime;
            Debug.Log("Using ability");
            yield return waitForFixedUpdate;
        }

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        cooldownTime = ability.cooldownTime;
        while(cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
            Debug.Log("On cooldown");
            yield return waitForFixedUpdate;
        }
        usable = true;
    }

    #endregion

    #region Held Ability
    
    private void UseHeldAbility()
    {
        if(usable && photonView.IsMine)
        {
            if(ability.Activate(gameObject)){StartCoroutine(ActiveCoroutine());}
        }
    }
    //needs to call something that will decrease the life of the ability
    //when the life is 0, it will stop the ability
    //when the ability is stopped, it will raise the cooldown

    private IEnumerator HeldCoroutine()
    {
        while(true)
        {
            ability.DuringDurtion(gameObject);
            yield return waitForFixedUpdate;
        }
    }
    #endregion
    public void Test()
    {
        Debug.Log("Test");
    }
}
}
