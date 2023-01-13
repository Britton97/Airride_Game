using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineUpdate : MonoBehaviour
{
    private WaitForFixedUpdate waitUntilNextFrame = new WaitForFixedUpdate();
    private WaitForSeconds waitTime;
    public bool updateOnFixedUpdate = false;
    public float timeBetweenUpdates = 0.1f;

    private Coroutine updateCoroutine;

    public UnityEvent startEvent;
    public UnityEvent waitForSecondsUpdateEvent;
    public UnityEvent fixedUpdateEvent;

    public UnityEvent endEvent;

    private void Awake()
    {
        waitTime = new WaitForSeconds(timeBetweenUpdates);
        startEvent.Invoke();
    }

    public void StartUpdate()
    {
        if (updateOnFixedUpdate)
        {
            updateCoroutine = StartCoroutine(WaitForFixedUpdateCoroutine());
        }
        else
        {
            updateCoroutine = StartCoroutine(WaitForSecondsCoroutine());
        }
    }

    public IEnumerator WaitForSecondsCoroutine()
    {
        while (true)
        {
            waitForSecondsUpdateEvent.Invoke();
            yield return waitTime;
        }
    }

    public IEnumerator WaitForFixedUpdateCoroutine()
    {
        while (true)
        {
            fixedUpdateEvent.Invoke();
            yield return waitUntilNextFrame;
        }
    }

    public void EndUpdate()
    {
        StopCoroutine(updateCoroutine);
        endEvent.Invoke();
    }
}
