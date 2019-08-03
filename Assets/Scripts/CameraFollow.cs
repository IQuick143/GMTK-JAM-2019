using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference "Smooth Camera Follow in Unity - Tutorial, by Brackeys"
//https://www.youtube.com/watch?v=MFQhpwc6cKE
//Comments say lerp is not good for cameras, use SmoothDamp instead

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float distanceDamp;

    private Vector3 velocity = Vector3.one;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;

    private void LateUpdate() {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, distanceDamp);
    }
}
