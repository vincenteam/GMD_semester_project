using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive: MonoBehaviour
{
    // booleans to make sure some actions are performed only once
    private bool _suicided = false;
    private bool _dead = false;
    
    public delegate void ActionsDelegate();
    public delegate void TerminateDelegate(GameObject go);
    
    private ActionsDelegate _suicide;
    private ActionsDelegate _onDeath;
    private ActionsDelegate _onDeathActionsExit;
    private TerminateDelegate _terminate;

    public ActionsDelegate OnSuicide
    {
        get => _suicide;
        set => _suicide = value;
    }
    
    public ActionsDelegate OnDeath
    {
        get => _onDeath;
        set => _onDeath = value;
    }
    
    public ActionsDelegate OnDeathActionsExit
    {
        get => _onDeathActionsExit;
        set => _onDeathActionsExit = value;
    }
    public TerminateDelegate Terminate
    {
        get => _terminate;
        set => _terminate = value;
    }

    public void Suicide()
    {
        if (!_suicided)
        {
            _suicided = true;
            _suicide();
            Die();   
        }
    }

    public void Die()
    {
        if (!_dead) // you can only die once
        {
            _dead = true;
            _onDeath();
        }
    }

    public void TerminateDeath()
    {
        _onDeathActionsExit();
        _terminate(gameObject);
    }
}
