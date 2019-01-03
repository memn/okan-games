using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;

    public float smoothSpeed = 0.125f;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            // Vector3 smoothPos = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
            // transform.position = smoothPos;
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }

    public void setTarget(Transform transform)
    {
        target = transform;
    }
}
