using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;


    private void Start()
    {
        // Nothing to do here
    }

    void FixedUpdate()
    {
        //where we want to go:
        Vector3 desiredPosition = target.position + offset;

        //the next step to get there:
        //where we are, where we're going, and how much we want to smooth by
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothPosition.z = -10;

        //move us to that next step
        transform.position = smoothPosition;
    }
}