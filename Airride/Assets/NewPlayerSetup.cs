using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class NewPlayerSetup : MonoBehaviour
{
    public PlayerInfoContainer newPlayerInfo;
    [SerializeField] private PlayerLobby playerLobby;
    public UnityEvent OnPlayerNameSet;
    public UnityEvent OnPlayerNameNotSet;
    [SerializeField] private TMP_InputField nameInputField;
    // Start is called before the first frame update
    void Start()
    {
        newPlayerInfo = ScriptableObject.CreateInstance<PlayerInfoContainer>();
    }

    public void TestPlayerName()
    {
       if (playerLobby.CheckLobbyForName(newPlayerInfo.GetPlayerName())) //Name unable to be found / joined
       {
              OnPlayerNameSet.Invoke();
       }
       else
       {
              OnPlayerNameNotSet.Invoke();
       }

    }
}
