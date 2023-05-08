using UnityEngine;

public class Alive: MonoBehaviour
{
    // booleans to make sure some actions are performed only once
    private bool _dead = false;

    public bool Dead
    {
        get => _dead;
        set => _dead = value;
    }
    
    public delegate void DeathDelegate(DamageTypes damageType);
    public delegate void TerminateDelegate(GameObject go);
    
    private DeathDelegate _onDeath;
    private DeathDelegate _onDeathDeathExit;
    private TerminateDelegate _terminate;
    private bool deathDone;

    public DeathDelegate OnDeath
    {
        get => _onDeath;
        set => _onDeath = value;
    }
    
    public DeathDelegate OnDeathDeathExit
    {
        get => _onDeathDeathExit;
        set => _onDeathDeathExit = value;
    }
    public TerminateDelegate Terminate
    {
        get => _terminate;
        set => _terminate = value;
    }

    public void Die(DamageTypes damageType)
    {
        if (!_dead) // you can only die once
        {
            _dead = true;
            if(_onDeath != null) _onDeath(damageType);
        }
    }

    public void TerminateDeath(DamageTypes damageType)
    {
        if (!deathDone)
        {
            if(_onDeathDeathExit != null) _onDeathDeathExit(damageType);
            if(_terminate != null) _terminate(gameObject);
            deathDone = true;
        }
    }
}
