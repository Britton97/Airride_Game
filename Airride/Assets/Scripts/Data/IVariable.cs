using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.MyCompany.MyGame
{
public interface IVariable<T>
{
    T Value { get; set; }
}
}
