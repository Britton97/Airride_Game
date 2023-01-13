using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Receiver : MonoBehaviour
{
    public Sender senderObject;
    public UnityEvent receivedActionSignal;

    private void Awake()
    {
        senderObject.sendAction += SenderCalled;
    }

    private void SenderCalled()
    {
        receivedActionSignal?.Invoke();
    }
}
