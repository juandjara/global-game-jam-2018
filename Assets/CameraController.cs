using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float x;
    public float y;
    public float z;
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;


    void Update () {
        Vector3 targetPosition = target.TransformPoint(new Vector3(x, y, z));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
