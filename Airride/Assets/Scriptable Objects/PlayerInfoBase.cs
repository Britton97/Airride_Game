using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInfoBase : ScriptableObject
{
    public abstract T GetData<T>();
}
