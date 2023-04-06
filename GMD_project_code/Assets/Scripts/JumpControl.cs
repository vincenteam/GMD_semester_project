using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    [SerializeField] private float checkDistance;
    private Collider _collider;

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
    }

    public bool CanJump()
    {
        float y = -_collider.bounds.size.y/2;
        Vector3 start = new Vector3(0, y, 0);
        start = transform.TransformPoint(start);
        
        Debug.DrawRay(start, -transform.up*checkDistance, Color.blue, 1);
        return Physics.Raycast(start, -transform.up, checkDistance);
        
    }
}
