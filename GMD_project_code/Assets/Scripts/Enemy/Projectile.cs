using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject hitVisual;
    
        [SerializeField] private float speed;

        [SerializeField] private float maxTravelDistance = 3;
        private float _distanceDone = 0;
        [SerializeField] private int damage;
        private Vector3 _move;

        private int _blockingLayers;

        private void Awake()
        {
            _blockingLayers = LayerMask.GetMask("Level", "Objects");
        }

        void FixedUpdate()
        {
            float step = Time.deltaTime * speed;
            transform.Translate(Vector3.forward * step);

            _distanceDone += step;

            if (_distanceDone > maxTravelDistance)
            {
                Invoke(nameof(Kill), 0);
            }
        }
        
        void OnTriggerEnter(Collider other)
        {
            GameObject g = other.GameObject();
            if (Convert.ToBoolean((1<<g.layer) & _blockingLayers))
            {
                Kill();   
            }else
            {
                Alive l = g.GetComponent<Alive>();
                if(l != null)
                {
                    l.Die();
                    Kill(); // no penetration
                }
            }
        }

        private void Kill()
        {
            var transform1 = transform;

            Quaternion rotation = transform1.rotation;
            rotation.y += 180;
            GameObject particles = Instantiate(hitVisual, transform1.position, rotation);
            Destroy(gameObject);
        }
    }
}
