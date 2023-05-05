using UnityEngine;

namespace LevelItems
{
    public class Bumper : MonoBehaviour
    {
        [SerializeField] private int force;
        [SerializeField] private AudioSource audioSourceBump;
        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb is not null)
            {
                audioSourceBump.Play();
                rb.AddForce(transform.up*force, ForceMode.Impulse);
            }
        }
    }
}
