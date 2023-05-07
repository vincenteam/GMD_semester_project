using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector2 : MonoBehaviour
{
    [SerializeField] private Collider feet;
    private List<Collider> _groundColliders = new();
    private Dictionary<Collider, int> _individualColliderCount = new(); // I'm going in hell for this (me and the guy responsible for Unity joints)
    private bool _grounded = false;
    [SerializeField] private float groundTiltLimit;
    
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;

    private int _maxColliders = 100;
    private bool _inTimeOut = false;

    [SerializeField]private float
    timeOutLenght;
    private bool _groundCurrentlyDetected = false;
    
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
                _groundColliders.Add(coll);
                break;
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
        }
    }
    

    private void LeaveGround()
    {
        print("leave ground");
        if (_leaveGround != null) _leaveGround();
        _grounded = false;
    }

    private void Update()
    {
        var position = transform.position;
        Vector3 spherePosition = new Vector3(position.x, position.y - GroundedOffset,
            position.z);

        Collider[] colliders = new Collider[_maxColliders];

        int nbColliders = Physics.OverlapSphereNonAlloc(spherePosition, GroundedRadius, colliders, ~LayerMask.GetMask("Living"), QueryTriggerInteraction.Ignore);

        //print("" + nbColliders);
        _groundCurrentlyDetected = false;
        for (int i=0; i < nbColliders; i++)
        {
            _groundCurrentlyDetected = true;
            if (!_grounded && !_inTimeOut)
            {
                print("start timeout");
                Invoke(nameof(Land), timeOutLenght);
                _inTimeOut = true;
            }
        }
        
        if(!_groundCurrentlyDetected)
        {
            if (_grounded)
            {
                LeaveGround();
            }
            
            _grounded = false;
        }
    }

    private void Land()
    {
        print("end timeoute");
        _inTimeOut = false;
        if (_groundCurrentlyDetected)
        {
            print("land");
            _grounded = true;
            if (_land != null) _land(); 
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
            GroundedRadius);
    }
}
