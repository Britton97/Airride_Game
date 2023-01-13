using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Data : ScriptableObject
{
    public abstract T ReturnData<T>();
    public abstract T SetData<T>(T data);
}
