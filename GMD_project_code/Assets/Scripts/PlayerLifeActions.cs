using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeActions : MonoBehaviour
{
    private static readonly int Suicide = Animator.StringToHash("suicide");
    
    [SerializeField] private Alive lifeEvents;
    [SerializeField] private GameObject body; // prefab

    private SkinManager _skinManager;
    private MeshRenderer _renderer;
    private Rigidbody _rb;

    private void Awake()
    {
        _skinManager = gameObject.GetComponent<SkinManager>();
        _renderer = gameObject.GetComponent<MeshRenderer>();
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        lifeEvents.OnSuicide += OnSuicide;
        lifeEvents.OnDeath += OnDeath;
        lifeEvents.Terminate = Destroy;
    }

    private void OnSuicide()
    {
        _skinManager.AnimatorInstance.SetTrigger(Suicide);
    }

    private void OnDeath()
    {
        int layer = LayerMask.NameToLayer("Ignore_moving");
        gameObject.layer = layer; // still collide with the body (layer change happens too late ?)
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;
        }

        GameObject newBody = Instantiate(body, transform.position, transform.rotation);
        //MeshRenderer bodyRenderer = newBody.GetComponent<MeshRenderer>();

        SkinManager bodySkinManager = newBody.GetComponent<SkinManager>();
        Rigidbody rbBody = newBody.GetComponent<Rigidbody>();
        
        rbBody.AddForce(_rb.velocity, ForceMode.VelocityChange);
        bodySkinManager.ChangeSkin(_skinManager.SkinInstance);
        
        SkinnedMeshRenderer[] renderers = newBody.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var rend in renderers) { rend.enabled = false; }
        //enable body at the end of suicide_in animation
        NotifyEnd notifier = _skinManager.AnimatorInstance.GetBehaviour<NotifyEnd>();
        notifier.OnAnimEnd += delegate
        {
            SkinnedMeshRenderer[] renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var rend in renderers) { rend.enabled = false; }
            renderers = newBody.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var rend in renderers) { rend.enabled = true; }
        };
        

        Invoke(nameof(TerminateDeath), 2.5f);
    }

    private void TerminateDeath()
    {
        lifeEvents.TerminateDeath();
    }
}
