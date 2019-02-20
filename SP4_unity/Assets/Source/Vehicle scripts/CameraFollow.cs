using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    public Vector3 offset;                     // The initial offset from the target.


    void Start()
    {
        
    }

    public IEnumerator LoadCamera()
    {
        target = GameObject.FindGameObjectWithTag("Player" + ((int)PlayerManager.playerManager.GetPlayerID((int)PlayerManager.playerManager.GetPlayerIndex()) + 1)).transform;
        // Calculate the initial offset.
        offset = transform.position - target.position;

        yield return null;
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
