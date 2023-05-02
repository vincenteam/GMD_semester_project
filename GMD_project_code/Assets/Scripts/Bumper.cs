using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private int force;
    [SerializeField] private AudioSource _audioSourceBump;
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb is not null)
        {
            _audioSourceBump.Play();
            rb.AddForce(transform.up*force, ForceMode.Impulse);
        }
    }
}
