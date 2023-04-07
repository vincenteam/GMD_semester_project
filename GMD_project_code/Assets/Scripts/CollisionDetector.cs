using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private int collisionCount;
    private bool grounded = false;
    
    public delegate void ActionsDelegate();
    private ActionsDelegate _land;
    public ActionsDelegate OnLand
    {
        get => _land;
        set => _land = value;
    }
    
    private ActionsDelegate _leaveGround;
    public ActionsDelegate OnLeaveGround
    {
        get => _leaveGround;
        set => _leaveGround = value;
    }
    
    public bool Grounded
    {
        get => grounded;
    }

    public void ForceCollisionOut()
    {
        LeaveGround();
    }

    void OnTriggerEnter(Collider other)
    {
        if (collisionCount == 0) Land();
        collisionCount++;
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--;
        if (collisionCount == 0) LeaveGround();
    }

    private void LeaveGround()
    {
        if (grounded) _leaveGround();
        grounded = false;
    }

    private void Land()
    {
        if (!grounded) _land(); 
        grounded = true;
    }
}
