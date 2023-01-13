using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfoContainer : ScriptableObject
{
    [SerializeField] private Base_Data playerName;

    public string GetPlayerName()
    {
        return playerName.ReturnData<string>();
    }
}
