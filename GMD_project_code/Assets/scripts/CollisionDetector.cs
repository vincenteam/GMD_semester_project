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

    public void ForceCollisionOut()
    {
        print("force out " + collisionCount);
        grounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        collisionCount++;
        print("enter " + collisionCount);
        grounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--;
        print("exit " + collisionCount);
        if (collisionCount < 0)
        {
            print("niggatif");
        }
        grounded = collisionCount > 0;
    }
}
