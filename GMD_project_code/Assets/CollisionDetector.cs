using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool grounded = false;

    public bool Grounded
    {
        get => grounded;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        grounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        grounded = false;
    }
}
