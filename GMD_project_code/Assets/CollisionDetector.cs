using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private int collisionCount;

    public bool Grounded
    {
        get => collisionCount > 0;
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
        collisionCount++;
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--;
    }
}
