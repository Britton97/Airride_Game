using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerID", menuName = "Scriptable Objects/Player ID")]
public class PlayerID : PlayerInfoBase
{
    public string playerStringID;

    public override T GetData<T>()
    {
        if (typeof(T) == typeof(string))
        {
            return (T)(object)playerStringID;
        }
        else
        
        {
            Debug.LogError("Wrong type");
            return default;
        }
    }
}
