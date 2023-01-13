using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLobby", menuName = "Scriptable Objects/PlayerLobby")]
public class PlayerLobby : ScriptableObject
{
    private List<PlayerInfoContainer> playerLobby = new List<PlayerInfoContainer>();

    public bool CheckLobbyForName(string suggestedName)
    {
        if(playerLobby.Count == 0)
        {
            return true;
        }

        foreach(PlayerInfoContainer player in playerLobby)
        {
            if(player.GetPlayerName() == suggestedName)
            {
                return false;
            }
        }

        return true;
    }

    public void AddPlayerToLobby(PlayerInfoContainer player)
    {
        playerLobby.Add(player);
    }
}
