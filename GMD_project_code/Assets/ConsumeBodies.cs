using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class ConsumeBodies : MonoBehaviour
{
    [SerializeField] private float consumeRate;
    [SerializeField] private string bodiesTag;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bodiesTag))
        {
            print("start running");
            collision.rigidbody.isKinematic = true;
            StartCoroutine(ConsumeBody(collision.collider));
        }
    }

    private IEnumerator ConsumeBody(Collider col)
    {
        float size = col.bounds.size.y;
        float consumed = 0;
        while (consumed < size)
        {
            float step = consumeRate * Time.deltaTime;
            col.transform.Translate(step*Vector3.down, Space.World); // moveposition
            consumed += step;
            
            yield return null;
        }

        Destroy(col.gameObject);
    }
}
