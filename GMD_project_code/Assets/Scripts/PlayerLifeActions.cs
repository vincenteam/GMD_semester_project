using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeActions : MonoBehaviour
{
    [SerializeField] private Alive lifeEvents;
    [SerializeField] private GameObject body; // prefab

    
    private Animator _animator;
    
    private FollowPlayer _camManager;
    private SwitchPosition _camView;
    
    
    private static readonly int Suicide = Animator.StringToHash("suicide");

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        lifeEvents.OnSuicide += OnSuicide;
        lifeEvents.OnDeath += OnDeath;
        lifeEvents.Terminate = Destroy;
        
        GameObject camGo = Tools.GetTransformByTag(transform, "MainCamera").gameObject;
        if (camGo) _camManager = camGo.GetComponent<FollowPlayer>();
        if (camGo) _camView = camGo.GetComponent<SwitchPosition>();
    }

    private void OnSuicide()
    {
        _animator.SetTrigger(Suicide);
    }

    private void OnDeath()
    {
        //gameObject.layer = LayerMask.NameToLayer("Ignore_moving"); // still collide with the body (layer change happens too late ?)
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        foreach (Collider c in gameObject.GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }

        
        _camView.SwitchTo("3person");
        Instantiate(body, transform.position, transform.rotation);
        
        Invoke("TerminateDeath", 2.5f);
    }

    private void TerminateDeath()
    {
        _camManager.UnLock();
        lifeEvents.TerminateDeath();
    }
}
