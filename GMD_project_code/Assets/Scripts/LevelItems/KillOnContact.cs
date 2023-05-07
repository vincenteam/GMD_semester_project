using UnityEngine;

namespace LevelItems
{
    public class KillOnContact : MonoBehaviour
    {
        [SerializeField] private DamageTypes dyingType = DamageTypes.Suicide;
        
        private void OnTriggerEnter(Collider other)
        {
            Alive life = other.gameObject.GetComponentInParent<Alive>();
            if (life)
            {
                life.Die(dyingType);
            }
        }
    }
}
