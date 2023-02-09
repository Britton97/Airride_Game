using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
public class Tag_Ability_Logic : MonoBehaviourPunCallbacks
{
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] private Tag_Ability tagAbility;

    [SerializeField] private Data_Float playerSpeed;
    private float speedMultiplier = 1f;
    private float rotateSpeedMultiplier = 1f;
    private WaitForFixedUpdate waitForFixedUpdate;
    private float timeSinceStarted = 0f;

    private void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed * rotateSpeedMultiplier, 0);
    }

    public void SpeedUp()
    {
        photonView.RPC("SpeedUpRPC", RpcTarget.AllViaServer);
    }
    [PunRPC]
    private void SpeedUpRPC()
    {
        StartCoroutine(SpeedUpRotation());
    }

    public void SpeedDown()
    {
        photonView.RPC("SpeedDownRPC", RpcTarget.AllViaServer);
    }
    [PunRPC]
    private void SpeedDownRPC()
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
            rotateSpeedMultiplier = tagAbility.rotateSpeedUpCurve.Evaluate(timeSinceStarted);
            playerSpeed.Value = speedMultiplier;
            yield return waitForFixedUpdate;
        }
    }

    [PunRPC]
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
            rotateSpeedMultiplier = tagAbility.rotateSpeedDownCurve.Evaluate(timeSinceStarted);
            playerSpeed.Value = speedMultiplier;
            yield return waitForFixedUpdate;
        }
    }

    public void SetParent(int parentId)
    {
        //transform.SetParent(PhotonView.Find(parentId).transform);
        photonView.RPC("SetParentRPC", RpcTarget.AllBuffered, parentId);
    }

    [PunRPC]
    private void SetParentRPC(int parentId)
    {
        transform.SetParent(PhotonView.Find(parentId).transform);
    }
}
}
