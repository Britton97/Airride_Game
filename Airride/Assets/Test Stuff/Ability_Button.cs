using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Button : MonoBehaviour
{
    public static Action AbilityUsed;
    
    public void ButtonPressed()
    {
        AbilityUsed?.Invoke();
    }
}
