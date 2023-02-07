using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVariable<T>
{
    T Value { get; set; }
}
