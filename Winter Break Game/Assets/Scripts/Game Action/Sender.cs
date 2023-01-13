using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Sender", menuName = "Sender/Sender Base")]
public class Sender : ScriptableObject
{
    public UnityAction sendAction;

    public void SendAction()
    {
        sendAction?.Invoke();
    }
}