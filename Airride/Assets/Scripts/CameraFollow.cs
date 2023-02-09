using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera followCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartFollowing()
    {
        followCam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        followCam.Follow = this.gameObject.transform;
        followCam.LookAt = this.gameObject.transform;
    }
}
