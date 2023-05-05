using System;
using System.Collections;
using System.Collections.Generic;
using LevelItems;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    [SerializeField] private ButtonPressed button;
    
    private void Start()
    {
        button.OnButtonDown += FlyUp;
        button.OnButtonDown += FlyInformations;
        print(button);
    }

    private void FlyUp()
    {
        this.transform.position += Vector3.up;
    }

    private void FlyInformations()
    {
        print("The cube is flying higher than ever ! Its position is now at y = " + this.transform.position.y);
    }
}
