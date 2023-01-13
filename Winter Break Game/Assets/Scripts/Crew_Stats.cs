using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crew Stats", menuName = "Crew/Individual")]
public class Crew_Stats : ScriptableObject
{
    public string crewName;
    public int angerValue; //red value
    public int digustValue; //green value
    public int sadnessValue; //blue value

    public Color crewColor;

    private Crew_To_World crewToWorld;
    public void AdjustEmotions(EmotionType emotionType, int amount)
    {
        switch (emotionType)
        {
            case EmotionType.Anger:
                angerValue += amount;
                break;
            case EmotionType.Disgust:
                digustValue += amount;
                break;
            case EmotionType.Sadness:
                sadnessValue += amount;
                break;
        }

        LimitValues();
        SetCrewColor();
    }

    private void LimitValues()
    {
        //angerValue, disgustValue, sadnessValue = Mathf.Clamp(angerValue, 0, 100);
        //if angerValue is less than 0, set it to 0 or above 255, set it to 255
        angerValue = Mathf.Clamp(angerValue, 0, 255);
        digustValue = Mathf.Clamp(digustValue, 0, 255);
        sadnessValue = Mathf.Clamp(sadnessValue, 0, 255);
    }

    private void SetCrewColor()
    {
        crewColor.r = angerValue;
        crewColor.g = digustValue;
        crewColor.b = sadnessValue;
    }

    public void OnStart(GameObject passedObj)
    {
        crewToWorld = passedObj.GetComponent<Crew_To_World>();
    }
}
