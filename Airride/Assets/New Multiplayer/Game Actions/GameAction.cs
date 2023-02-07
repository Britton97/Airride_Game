using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class GameAction : ScriptableObject
{
    public Action actionEvent;
    public void InvokeGameAction()
    {
        actionEvent.Invoke();
    }
}