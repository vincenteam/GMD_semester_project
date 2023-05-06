using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class SniperActions : MonoBehaviour
    {
        private Loadout _loadout;
        private EnemyMovement _movement;
        private GunMovement _gunMovement;
        
        
        private void Awake()
        {
            _loadout = GetComponent<Loadout>();
            _movement = GetComponent<EnemyMovement>();
            _gunMovement = GetComponentInChildren<GunMovement>();
        }


        public IEnumerator Track(GameObject target)
        {
            while (true)
            {
                if (target == null) break;
                var position = target.transform.position;
                _gunMovement.AimTowardsX(position);
                _movement.RotateTowards(position);
                yield return null;
            }
        }
        
        public IEnumerator DumbShoot()
        {
            while (true)
            {
                FireArm gun = _loadout.Gun;
                if (gun.Ammo <= 0)
                {
                    gun.StartReload();
                }
                else
                {
                    gun.Fire();
                }
                yield return new WaitForSeconds(1f);   
            }
        }
    }
}
