using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Station Behavior", menuName = "Station Behavior/Increment Emotion")]
public class Station_Inc_Emotion : Station_Behavior_Base
{
    [SerializeField] private Crew_List crewList;
    [SerializeField] private EmotionType emotionType;
    [SerializeField] private ToWho stationAffects;
    public int incrementAmount = 1;
    public override void OnStart()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        //on update needs to send an action to the crew manager
        //the action needs to carry without info about which emotion to affect
        //who it affects
        //and how much to affect it by
        crewList.ChangeCrewEmotions(stationAffects, emotionType, incrementAmount);
    }

    public override void OnEnd()
    {
        //throw new System.NotImplementedException();
    }
}
