using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
public abstract class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public abstract bool Activate(GameObject parent);
    public abstract void DuringDurtion(GameObject parent);
    public abstract void Deactivate(GameObject parent);
}
}
