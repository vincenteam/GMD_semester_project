using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collide");
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            print("rb found");
            Vector3 force = transform.forward;
            rb.AddForce(force*1000);
        }
    }
}
