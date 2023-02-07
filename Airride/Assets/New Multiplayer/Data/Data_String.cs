using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New String Data", menuName = "Data/String")]
public class Data_String : Data_Base, IVariable<string>
{
    [SerializeField] private string value;

    public string Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
