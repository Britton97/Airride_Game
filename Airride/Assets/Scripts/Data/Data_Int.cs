using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int Data", menuName = "Data/Integer")]
public class Data_Int : Data_Base, IVariable<int>
{
    [SerializeField] private int value;

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
