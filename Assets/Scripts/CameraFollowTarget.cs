using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 1f;
    private Vector3 offset;
    void Start()
    {
        if (target)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed);
        }
    }
}
