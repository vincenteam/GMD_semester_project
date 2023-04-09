using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private float power;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 force = transform.forward;
            rb.AddForce(force*power, ForceMode.Impulse);
        }
    }

    /*private void OnCollisionStay(Collision collisionInfo)
    {
        Rigidbody rb = collisionInfo.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //print("rb found");
            Vector3 force = transform.forward;
            rb.AddForce(force*1, ForceMode.Impulse);
        }
    }*/
}
