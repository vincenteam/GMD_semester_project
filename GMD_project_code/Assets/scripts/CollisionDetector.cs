using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private int collisionCount;
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

    public void ForceCollisionOut()
    {
        grounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        collisionCount++;
        grounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--;
        grounded = collisionCount > 0;
    }
}
