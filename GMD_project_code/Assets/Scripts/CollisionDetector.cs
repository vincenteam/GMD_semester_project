using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private int _collisionCount;
    private bool _grounded = false;
    
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
        get => _grounded;
    }

    public void ForceCollisionOut()
    {
        LeaveGround();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_collisionCount == 0) Land();
        _collisionCount++;
    }

    void OnTriggerExit(Collider other)
    {
        _collisionCount--;
        if (_collisionCount == 0) LeaveGround();
    }

    private void LeaveGround()
    {
        if (_grounded) _leaveGround();
        _grounded = false;
    }

    private void Land()
    {
        if (!_grounded) _land(); 
        _grounded = true;
    }
}
