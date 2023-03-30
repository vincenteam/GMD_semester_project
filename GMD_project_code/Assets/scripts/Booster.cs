using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private CharacterController Controller;
    
    [SerializeField] private int forceAmount = 1000;
    
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
        //Controller = collision.gameObject.GetComponent<CharacterController>();
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        //Controller.Move(forceAmount * Time.deltaTime * transform.TransformDirection(Vector3.forward));
        if (rb != null)
        {
            rb.AddForce(transform.forward*forceAmount*Time.deltaTime);
        }
    }
}