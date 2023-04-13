using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] private ButtonInterface reactingObject;
    
    private void OnCollisionEnter()
    {
        print("collided with button");
        reactingObject.React();
    }
}
