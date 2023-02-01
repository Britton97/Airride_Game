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
        Ability_Button.AbilityUsed += UseAbility;
    }

    enum AbilityState
    {
        Ready,
        Cooldown
    }

    AbilityState state = AbilityState.Ready;

    void UseAbility()
    {
        if(usable && photonView.IsMine)
        {
            ability.Activate(gameObject);
            StartCoroutine(ActiveCoroutine());
        }
    }

    #region Coroutines

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
}
}
