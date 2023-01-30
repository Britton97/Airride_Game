using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
public class Dash_ : MonoBehaviourPunCallbacks
{
    [HideInInspector] NewPlayerMovement playerMovement;
    public float dashSpeed = 10f;
    public float dashTime = 0.25f;
    float startTime;

    private PlayerManager target;

    void Start()
    {
        playerMovement = GetComponent<NewPlayerMovement>();
        PlayerAbilities.AbilityUsed += StartDash;
    }

    public void StartDash()
    {
        if(photonView.IsMine)
        {            
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            playerMovement.characterController.Move(playerMovement.transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void SetTarget(PlayerManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        target = _target;
    }
}
}
