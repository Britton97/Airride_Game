using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Later : MonoBehaviour
{
    public PlayerInfoBase playerInfoBase;
    // Start is called before the first frame update
    void Start()
    {
        string thing = playerInfoBase.GetData<string>();
        Debug.Log(thing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
