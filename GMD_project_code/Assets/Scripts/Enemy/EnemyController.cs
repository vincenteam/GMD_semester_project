using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private AudioSource alertSound;
        private void Awake()
        {
            Guarding guarding = GetComponent<Guarding>();
            if (guarding != null)
            {
                guarding.OnAlertedStart += _ => { alertSound.Play(); };
            }
        }
    }
}
