using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [ContextMenuItem("Test", "Test")]
    public Data_Float test;

    void Start()
    {
        //call get value from test
    }
    private void OnEnable()
    {
        //Actions.Ability1Used += Test;
    }
    private void OnDisable()
    {
        //Actions.Ability1Used -= Test;
    }
    public void Test()
    {
        //test.Value = 10;
        Debug.Log("called");
    }
}
