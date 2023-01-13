using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "String_Data", menuName = "Data Types/String")]
public class String_Data : Base_Data
{
    [SerializeField] private string data;
    public override T ReturnData<T>()
    {
        throw new System.NotImplementedException();
    }

    public override T SetData<T>(T data)
    {
        throw new System.NotImplementedException();
    }
}
