using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private float power;
    [SerializeField] private float maxVelocity;

    /*private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 force = transform.forward;
            rb.AddForce(force*power, ForceMode.Impulse);
        }
    }*/

    private void OnCollisionStay(Collision collisionInfo)
    {
        Rigidbody rb = collisionInfo.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (transform.InverseTransformDirection(rb.velocity).z < maxVelocity)
            {
                Vector3 direction = transform.forward;
                rb.AddForce(direction*power, ForceMode.Impulse);   
            }
        }
    }
}
