using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Alive life = other.gameObject.GetComponentInParent<Alive>();
        if (life)
        {
            life.Die();
        }
    }
}
