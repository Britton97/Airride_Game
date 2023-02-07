using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ability_Button : MonoBehaviour
{
    public static Action AbilityUsed;
    public static Action AbilityDeactivated;

    public void ButtonPressed()
    {
        AbilityUsed?.Invoke();
    }

    public void ButtonHeld()
    {
        Actions.Ability1Used();
    }

    public void ButtonReleased()
    {
        //AbilityDeactivated?.Invoke();
    }
}
