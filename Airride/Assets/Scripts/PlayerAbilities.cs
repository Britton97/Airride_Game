using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAbilities : MonoBehaviourPunCallbacks
{
    public static Action AbilityUsed;
    public void ButtonPressed()
    {
        AbilityUsed?.Invoke();
    }
}
