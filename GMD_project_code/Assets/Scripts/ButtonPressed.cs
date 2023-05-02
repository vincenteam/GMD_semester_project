using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> scriptsTarget = new();
    [SerializeField] private List<string> methodTarget = new();

    public delegate void ActionDelegate();
    private ActionDelegate _pressed;
    [SerializeField] private AudioSource audioSourcePressed;

    public ActionDelegate OnPressed
    {
        get => _pressed;
        set => _pressed += value;
    }

    private void Start()
    {
        foreach ((MonoBehaviour, string) kvp in scriptsTarget.Zip(methodTarget, (script, str) => (script, str)))
        {
            _pressed += (ActionDelegate)Delegate.CreateDelegate(typeof(ActionDelegate), kvp.Item1, kvp.Item2, true, false);
        }
    }

    public void Pressed()
    {
        audioSourcePressed.Play();
        _pressed();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (_pressed != null)
        {
            Pressed();
        }
        print("collision with button !");
    }
}
