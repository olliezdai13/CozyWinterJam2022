using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckSensor : MonoBehaviour
{
    [SerializeField] private Transform[] m_sensors;
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private float m_groundCheckDistance = 0.1f;

    public bool IsGrounded;

    void FixedUpdate()
    {
        IsGrounded = RaycastFromAllSensors();
    }

    private bool RaycastFromAllSensors()
    {
        foreach (var sensor in m_sensors)
        {
            if (RaycastFromSensor(sensor)) return true;
        }
        return false;
    }

    private bool RaycastFromSensor(Transform sensor)
    {
        RaycastHit hit;
        var pos = sensor.position;
        var forward = sensor.forward;
        Debug.DrawRay(pos, forward * m_groundCheckDistance);
        if (Physics.Raycast(pos, forward, out hit, m_groundCheckDistance, m_layerMask))
        {
            // Debug.DrawRay(pos, forward * m_groundCheckDistance);
            return true;
        }
        return false;
    }
}