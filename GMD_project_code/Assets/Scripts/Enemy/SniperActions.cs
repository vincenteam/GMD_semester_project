using System;
using System.Collections;
using GMDProject;
using UnityEngine;

namespace Enemy
{
    public class SniperActions : MonoBehaviour
    {
        [SerializeField] private int lookDistance;
        [SerializeField] string[] targetLayers = new[] { "Living" };
        [SerializeField] string[] obstacleLayers = new[] { "Level", "Objects" };

        private Loadout _loadout;
        private EnemyMovement _movement;
        
        
        private void Awake()
        {
            _loadout = GetComponent<Loadout>();
            _movement = GetComponent<EnemyMovement>();
        }


        public IEnumerator Track(GameObject target)
        {
            while (true)
            {
                if (target == null) break;
                
                _movement.RotateTowards(target.transform.position);
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
