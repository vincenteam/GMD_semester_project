using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Collider feet;
    private int _collisionCount;
    //private HashSet<Collider> _groundColliders = new();
    private List<Collider> _groundColliders = new();
    private Dictionary<Collider, int> _individualColliderCount = new(); // I'm going in hell for this (me and the guy responsible for Unity joints)
    private bool _grounded = false;
    [SerializeField] private float groundTiltLimit;
    
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

    private void OnCollisionEnter(Collision collision)
    {
        //print("new collision " + collision.collider.name);
        Collider coll = collision.collider;
        if (_individualColliderCount.ContainsKey(coll))
        {
            _individualColliderCount[coll]++;    
        }
        else
        {
            _individualColliderCount.Add(coll, 1);
        }


        for (int i=0; i < collision.contactCount; i++) // use getcontacts and an array
        {
            ContactPoint contact = collision.GetContact(i);
            if (contact.thisCollider == feet)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.blue, 1);

                float angle = Vector3.Angle(contact.normal, transform.up);
                //print("angle " + angle);
                if (-groundTiltLimit < angle && angle < groundTiltLimit)
                {
                    
                    //if (_groundColliders.Add(collision.collider))
                    //{
                    //print("match " + _groundColliders.Find(delegate(Collider collider1) { return collider1 == collision.collider; }));
                    _groundColliders.Add(coll);
                    //print("ground collision ");
                    
                        if (_collisionCount == 0) Land();
                        _collisionCount++;
                    //}
                    break;
                }
                else
                {
                    //print("feet collide wall");
                }
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Collider coll = other.collider;
        if (_individualColliderCount.ContainsKey(coll))
        {
            _individualColliderCount[coll]--;    
        }
        
        if (_groundColliders.Contains(coll) && _individualColliderCount[coll] == 0)
        {
            _groundColliders.Remove(coll);
            _collisionCount--;
            //print("collision out " + _collisionCount + " collider " + other.gameObject.name);
            if (_collisionCount == 0) LeaveGround();
            //if(_collisionCount < 0) print("negative collision count");
        }
    }


    public void ForceCollisionOut()
    {
        LeaveGround();
    }

    private void LeaveGround()
    {
        if (_grounded) _leaveGround();
        _grounded = false;
    }

    private void Land()
    {
        if (!_grounded) _land(); 
        //print("land");
        _grounded = true;
    }
}
