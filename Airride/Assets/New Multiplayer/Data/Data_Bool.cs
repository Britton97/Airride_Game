using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bool Data", menuName = "Data/Bool")]
public class Data_Bool : Data_Base, IVariable<bool>
{
    [SerializeField] private bool value;

    public bool Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
