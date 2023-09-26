using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject target;
    public LayerMask layerMask;
    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (target)
        {
            RaycastHit hit;
            if (Physics.Raycast(target.transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(target.transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                transform.position = new Vector3(target.transform.position.x, hit.point.y, target.transform.position.z);
            } else
            {
                transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            }
        }
    }
}