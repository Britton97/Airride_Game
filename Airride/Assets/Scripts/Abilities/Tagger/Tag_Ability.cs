using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Com.MyCompany.MyGame
{
[CreateAssetMenu(fileName = "New Tag", menuName = "Ability/Tag")]
public class Tag_Ability : Ability
{
    [Header("Player Speed Curves")]
    public AnimationCurve speedUpCurve;
    public AnimationCurve speedDownCurve;

    [Header("Rotate Speed Curves")]

    public AnimationCurve rotateSpeedUpCurve;
    public AnimationCurve rotateSpeedDownCurve;
    private Tag_Ability_Logic taggerCollider;


    public override bool Activate(GameObject parent)
    {
        taggerCollider = parent.GetComponentInChildren<Tag_Ability_Logic>();
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
