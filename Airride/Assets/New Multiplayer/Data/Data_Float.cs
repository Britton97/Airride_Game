using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float Data", menuName = "Data/Float")]
public class Data_Float : Data_Base, IVariable<float>
{
    [SerializeField] private float value;

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
