using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private XROrigin player;
    private Camera cameraVR;

    private void Start()
    {
        player = FindObjectOfType<XROrigin>();
        cameraVR = player.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        //transform.LookAt(transform.position + cameraVR.transform.rotation * Vector3.forward, cameraVR.transform.rotation * Vector3.up);
    }
}
