using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Alive life = other.gameObject.GetComponentInParent<Alive>();
        print("collide with " + other.gameObject.name + " " + life);
        if (life)
        {
            life.Die();
        }
    }
}
