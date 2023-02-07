using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
public class TaggerCollider : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;
    private float speedMultiplier = 1f;
    private WaitForFixedUpdate waitForFixedUpdate;
    [SerializeField] private Tag_Ability tagAbility;
    private float timeSinceStarted = 0f;

    private void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed * speedMultiplier, 0);
    }

    public void SpeedUp()
    {
        StartCoroutine(SpeedUpRotation());
    }

    public void SpeedDown()
    {
        StartCoroutine(SpeedDownRotation());
    }

    private IEnumerator SpeedUpRotation()
    {
        timeSinceStarted = 0f;
        //keep coroutine going until timeSinceStarted is greater then the length of the curve
        while (timeSinceStarted < tagAbility.speedUpCurve.keys[tagAbility.speedUpCurve.length - 1].time)
        {
            //increment timeSinceStarted by the time that has passed since the last frame
            timeSinceStarted += Time.deltaTime;
            //get the value of the curve at the current time
            speedMultiplier = tagAbility.speedUpCurve.Evaluate(timeSinceStarted);
            yield return waitForFixedUpdate;
        }
    }

    private IEnumerator SpeedDownRotation()
    {
        timeSinceStarted = 0f;
        //keep coroutine going until timeSinceStarted is greater then the length of the curve
        while (timeSinceStarted < tagAbility.speedDownCurve.keys[tagAbility.speedDownCurve.length - 1].time)
        {
            //increment timeSinceStarted by the time that has passed since the last frame
            timeSinceStarted += Time.deltaTime;
            //get the value of the curve at the current time
            speedMultiplier = tagAbility.speedDownCurve.Evaluate(timeSinceStarted);
            yield return waitForFixedUpdate;
        }
    }
}
}
