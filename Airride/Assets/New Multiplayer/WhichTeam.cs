using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.MyCompany.MyGame
{
[CreateAssetMenu(fileName = "WhichTeam", menuName = "Team")]
public class WhichTeam : ScriptableObject
{

    [SerializeField] public Team activeTeam;
    [SerializeField] private int teamNumber;
    
    [Tooltip("If this has a checkmark, then the player can't move")]
    public bool cantMove;
    [Header("If You touch X1 role, Then They Become Y1 role")]
    [SerializeField] private Team x1;
    [SerializeField] private Team y1;
    [SerializeField] private WhichTeam z1;

    [Header("If You touch X2 role, Then They Become Y2 role")]
    [SerializeField] private Team x2;
    [SerializeField] private Team y2;
    [SerializeField] private WhichTeam z2;

    [Header("If You touch X3 role, Then They Become Y3 role")]
    [SerializeField] private Team x3;
    [SerializeField] private Team y3;
    [SerializeField] private WhichTeam z3;
    

    public int TouchedPlayer(Team otherPlayersTeam)
    {
        if (otherPlayersTeam == x1)
        {
            return z1.teamNumber;
        }
        else if (otherPlayersTeam == x2)
        {
            return z2.teamNumber;
        }
        else if (otherPlayersTeam == x3)
        {
            return z3.teamNumber;
        }
        else
        {
            return -1;
        }

    }

    public void EnterState(PlayerManager playerManager)
    {
        playerManager.IsFrozen = cantMove;
    }

    public int GetTeamNumber()
    {
        return teamNumber;
    }

    public enum Team
    {
        Tagger,
        Runner,
        Frozen
    }
}
}
