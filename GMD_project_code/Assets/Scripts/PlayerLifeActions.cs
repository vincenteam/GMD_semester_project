using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeActions : MonoBehaviour
{
    private static readonly int Suicide = Animator.StringToHash("suicide");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    [SerializeField] private Alive lifeEvents;
    [SerializeField] private GameObject body; // prefab

    private SkinManager _skinManager;
    private Rigidbody _rb;
    
    private void Awake()
    {
        _skinManager = gameObject.GetComponent<SkinManager>();
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
        Animator animator = _skinManager.AnimatorInstance;
        animator.SetTrigger(Suicide);
        animator.SetBool(IsDead, true);
    }

    private void OnDeath()
    {
        //enable body at the end of suicide_in animation
        NotifyEnd notifier = _skinManager.AnimatorInstance.GetBehaviour<NotifyEnd>();
        notifier.OnAnimEnd += delegate
        {        
            int layer = LayerMask.NameToLayer("Ignore_moving");
            gameObject.layer = layer;
            foreach (Transform child in transform)
            {
                child.gameObject.layer = layer;
            }
            GameObject newBody = Instantiate(body, transform.position, transform.rotation);

            SkinManager bodySkinManager = newBody.GetComponent<SkinManager>();
            Rigidbody rbBody = newBody.GetComponent<Rigidbody>();
            
            bodySkinManager.ChangeSkin(_skinManager.SkinInstance);
            bodySkinManager.SkinInstance.SetActive(true);
            rbBody.AddForce(_rb.velocity, ForceMode.VelocityChange);

            
            _rb.isKinematic = true;
            _skinManager.SkinInstance.SetActive(false);
        };

        Invoke(nameof(TerminateDeath), 2.5f);
    }

    private void TerminateDeath()
    {
        lifeEvents.TerminateDeath();
    }
}
