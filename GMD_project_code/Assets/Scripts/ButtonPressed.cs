using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    public delegate void Delegate();
    private Delegate _pressed;

    public Delegate OnPressed
    {
        get => _pressed;
        set => _pressed += value;
    }

    public void Pressed()
    {
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
