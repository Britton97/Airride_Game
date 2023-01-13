using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Sender_Base : ScriptableObject
{
    public UnityAction sendAction;

    public abstract void SendAction();
}
