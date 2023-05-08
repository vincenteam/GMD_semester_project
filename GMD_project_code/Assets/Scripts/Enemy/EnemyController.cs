using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private AudioSource alertSound;
        private void Awake()
        {
            Guarding guarding = GetComponentInChildren<Guarding>();
            if (guarding != null)
            {
                print("add sound");
                guarding.OnAlertedStart += _ => { alertSound.Play(); print("sound"); };
            }
        }
    }
}
