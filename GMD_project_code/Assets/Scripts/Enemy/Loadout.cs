using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Loadout : MonoBehaviour
    {
        private FireArm _equippedGun;
        public FireArm Gun
        {
            get => _equippedGun;
        }
        [SerializeField] private List<GameObject> gunsPrefab = new ();
        
        private List<GameObject> _guns = new ();
        [SerializeField] private int equippedGunInd = 0;
    
        void Awake()
        {
            Transform mountPoint = Tools.GetTransformByTag(transform, "WeaponMountPoint");
            
            foreach (GameObject g in gunsPrefab)
            {
                var newGun = Instantiate(g, mountPoint != null ? mountPoint: transform);
                _guns.Add(newGun);
                newGun.SetActive(false);
            }

            if (0 >= equippedGunInd && equippedGunInd < _guns.Count)
            {
                _equippedGun = _guns[equippedGunInd].GetComponent<FireArm>();
                _equippedGun.gameObject.SetActive(true);
            }
        }

        public void Switch()
        {
            _equippedGun.gameObject.SetActive(false);

            equippedGunInd = (equippedGunInd + 1) % _guns.Count;
            _equippedGun = _guns[equippedGunInd].GetComponent<FireArm>();
        
            _equippedGun.gameObject.SetActive(true);
        }

        public void SwitchTo(int gunInd)
        {
            if (0 >= gunInd && gunInd < _guns.Count)
            {
                _equippedGun.gameObject.SetActive(false);
                
                equippedGunInd = gunInd;
                _equippedGun = _guns[equippedGunInd].GetComponent<FireArm>();
                _equippedGun.gameObject.SetActive(true);
            }
        }
    }
}
