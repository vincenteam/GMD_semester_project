using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GMDProject;
using UnityEngine;

public class PlayerLifeActions : MonoBehaviour, IAnimationEventHandler
{
    private static readonly int Suicide = Animator.StringToHash("suicide");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    [SerializeField] private Alive lifeEvents;
    [SerializeField] private GameObject body; // prefab

    private SkinManager _skinManager;
    private Rigidbody _rb;

    private bool _deathActionsDone;
    private AnimationClip _deathClip;
    private AnimationEvent _deathClipEvent;
    
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
        _deathActionsDone = false;
        
        PlayerInput inputManager = gameObject.GetComponent<PlayerInput>();
        if (inputManager) inputManager.enabled = false;

        //enable body at the end of animation
        // put an event on the next animation playing to instantiate the body at the end
        Animator animator = _skinManager.AnimatorInstance;
        animator.Update(Time.deltaTime);
        
        int animationBaseLayer = animator.GetLayerIndex("Base Layer");
        animator.Update(Time.deltaTime);
        AnimatorClipInfo[] clipsInfo = animator.GetNextAnimatorClipInfo(animationBaseLayer);

        if (clipsInfo.Length > 0)
        {
            _deathClip = clipsInfo[0].clip;
            
            _deathClipEvent = new AnimationEvent();
            _deathClipEvent.functionName = "NotifyAnimationEnd";
            _deathClipEvent.time = _deathClip.length;
            _deathClipEvent.objectReferenceParameter = this;

            _deathClip.AddEvent(_deathClipEvent);    
        }
        
        Invoke(nameof(TerminateDeath), 2.5f); // time could be animation time + constant
    }

    private void TerminateDeath()
    {
        AnimationEnd(); // when terminate death is called, death actions are done, even if the animation as not ended
        lifeEvents.TerminateDeath();
    }
    
    private void DeathActions()
    {
        int layer = LayerMask.NameToLayer("Ignore_moving");
        gameObject.layer = layer;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;
        }
        _rb.isKinematic = true;

        if (_deathClip != null)
        {
            _deathClip.events = Array.Empty<AnimationEvent>();
        }
        
        _deathClip = null;
        _deathClipEvent = null;
        
        GameObject newBody = Instantiate(body, transform.position, transform.rotation);
        SkinManager bodySkinManager = newBody.GetComponent<SkinManager>();
        Rigidbody rbBody = newBody.GetComponent<Rigidbody>();

        bodySkinManager.ChangeSkin(_skinManager.SkinInstance);
        bodySkinManager.SkinInstance.SetActive(true);
        
        rbBody.AddForce(_rb.velocity, ForceMode.VelocityChange);
        
        
        _skinManager.SkinInstance.SetActive(false);
    }

    public void AnimationEnd()
    {
        if (!_deathActionsDone)
        {
            DeathActions();
            _deathActionsDone = true;
        }
    }
}
