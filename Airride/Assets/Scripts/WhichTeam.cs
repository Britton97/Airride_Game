using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
[CreateAssetMenu(fileName = "WhichTeam", menuName = "Team")]
public class WhichTeam : ScriptableObject
{

    [SerializeField] public Team activeTeam;
    [SerializeField] private int teamNumber;
    [SerializeField] List<GameObject> roleUIs;
    [SerializeField] List<GameObject> roleObjects;

    #region Team Rules
    
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
    #endregion
    
    #region Touched Player
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

    #endregion
    
    #region Spawn Methods

    public void SpawnRoleUIs(GameObject parent)
    {
        if(roleUIs.Count == 0){return;}

        foreach (GameObject roleUI in roleUIs)
        {
            GameObject roleUIInstance = Instantiate(roleUI, parent.transform);
            roleUIInstance.transform.position = parent.transform.position;
        }
    }

    public List<GameObject> SpawnRoleObjects(GameObject parent)
    {
        if(roleObjects.Count == 0){return null;}
        List<GameObject> spawned = new List<GameObject>();

        foreach (GameObject roleObject in roleObjects)
        {
            Debug.LogError($"Got here. team is {activeTeam}");
            GameObject obj = PhotonNetwork.Instantiate(roleObject.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            spawned.Add(obj);
            obj.transform.SetParent(parent.transform);
        }
        return spawned;
    }

    #endregion
}
}