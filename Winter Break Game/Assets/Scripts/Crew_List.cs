using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crew_List", menuName = "Crew/List of Crew")]
public class Crew_List : ScriptableObject
{
    public List<Crew_Stats> crewList = new List<Crew_Stats>();

    public void ChangeCrewEmotions(ToWho affectsWho, EmotionType emotionType, int amount)
    {
        //if affectsWho is everyone, then change all crew members
        if (affectsWho == ToWho.EntireCrew)
        {
            foreach (Crew_Stats crew in crewList)
            {
                crew.AdjustEmotions(emotionType, amount);
            }
        }
    }
}
