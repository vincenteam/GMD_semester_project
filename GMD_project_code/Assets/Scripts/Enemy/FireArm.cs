using UnityEngine;

namespace Enemy
{
    public class FireArm : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;

        [SerializeField] private float coolDown;
        [SerializeField] private float reloadTime;
    
        [SerializeField] private int clipSize;
        [SerializeField] private int ammoCapacity;
        private int _clip;
        private int _ammo;
    
        private bool _inCoolDown = false;

        private void Start()
        {
            // start with full clip and ammo by default
            _clip = clipSize;
            _ammo = ammoCapacity;
        }

        public void Fire()
        {
            if (!_inCoolDown && _clip > 0)
            {
                var transform1 = transform;
                Instantiate(bullet, transform1.position, transform1.rotation);
                _clip -= 1;

                _inCoolDown = true;
                Invoke(nameof(EndCoolDown), coolDown);
            }
        }

        public void EndCoolDown()
        {
            _inCoolDown = false;
        }

        public void StartReload()
        {
            Invoke(nameof(Reload), reloadTime);
            _inCoolDown = true;
        }

        public void RefillAmmo(int newAmmo)
        {
            int ammoNeeded = ammoCapacity - _ammo;
            if (newAmmo >= ammoNeeded)
            {
                _ammo += ammoNeeded;
            }
            else if (newAmmo > 0)
            {
                _ammo += newAmmo;
            }
        }
    
        private void Reload()
        {
            int ammoNeeded = clipSize - _clip;
            if (_ammo >= ammoNeeded)
            {
                _clip += ammoNeeded;
                _ammo -= ammoNeeded;
            }
            else if (_ammo > 0)
            {
                _clip += _ammo;
                _ammo = 0;
            }

            _inCoolDown = false;
        }

        public int GetAmmo()
        {
            return _ammo;
        }

        public bool AmmoIsFull()
        {
            return _ammo >= ammoCapacity;
        }
    }
}
