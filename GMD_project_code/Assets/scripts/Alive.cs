using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive: MonoBehaviour
{
    public delegate void ActionsDelegate();
    public delegate void TerminateDelegate(GameObject go);

    private ActionsDelegate onDeath;
    private ActionsDelegate onDeathActionsExit;
    private TerminateDelegate terminate;

    public ActionsDelegate OnDeath
    {
        get { return onDeath;}
        set { onDeath = value; }
    }
    
    public ActionsDelegate OnDeathActionsExit
    {
        get { return onDeathActionsExit;}
        set { onDeathActionsExit = value; }
    }
    public TerminateDelegate Terminate
    {
        get { return terminate;}
        set { terminate = value; }
    }

    public void Die()
    {
        onDeath();
    }

    public void TerminateDeath()
    {
        onDeathActionsExit();
        terminate(gameObject);
    }
}
