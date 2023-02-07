using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Com.MyCompany.MyGame
{
[CreateAssetMenu(fileName = "New Tag", menuName = "Ability/Tag")]
public class Tag_Ability : Ability
{
    public AnimationCurve speedUpCurve;
    public AnimationCurve speedDownCurve;
    private TaggerCollider taggerCollider;

    [Tooltip("How long the speed up lasts")]
    public Data_Float speedUpTime;
    public float recoverTime;

    public override bool Activate(GameObject parent)
    {
        taggerCollider = parent.GetComponentInChildren<TaggerCollider>();
        taggerCollider.SpeedUp();
        return true;
    }
    public override void DuringDurtion(GameObject parent)
    {
        return;
    }

    public override void Deactivate(GameObject parent)
    {
        taggerCollider.SpeedDown();
    }
}
}
