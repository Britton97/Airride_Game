using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WhichTeam", menuName = "Team")]
public class WhichTeam : ScriptableObject
{

    [SerializeField] public Team team;
    public enum Team
    {
        Tagger,
        Runner,
        Frozen
    }
}
