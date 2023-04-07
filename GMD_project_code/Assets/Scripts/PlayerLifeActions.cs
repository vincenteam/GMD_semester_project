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
    private static readonly int IsBody = Animator.StringToHash("isBody");

    private void Awake()
    {
        _skinManager = gameObject.GetComponent<SkinManager>();
        _renderer = gameObject.GetComponent<MeshRenderer>();
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
        //gameObject.layer = LayerMask.NameToLayer("Ignore_moving"); // still collide with the body (layer change happens too late ?)
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        foreach (Collider c in gameObject.GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
        
        GameObject newBody = Instantiate(body, transform.position, transform.rotation);
        newBody.SetActive(false);
        
        SkinManager bodySkinManager = newBody.GetComponent<SkinManager>();
        MeshRenderer bodyRenderer = newBody.GetComponent<MeshRenderer>();
        bodyRenderer.enabled = false;
        bodySkinManager.ChangeSkin(_skinManager.SkinInstance);

        //enable body at the end of suicide_in animation
        NotifyEnd notifier = _skinManager.AnimatorInstance.GetBehaviour<NotifyEnd>();
        notifier.OnAnimEnd += delegate
        {
            newBody.SetActive(true);
            bodyRenderer.enabled = true;
            _renderer.enabled = false;
        };
        

        Invoke(nameof(TerminateDeath), 2.5f);
    }

    private void TerminateDeath()
    {
        lifeEvents.TerminateDeath();
    }
}
