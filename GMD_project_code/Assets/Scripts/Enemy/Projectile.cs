using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject hitVisual;
    
        [SerializeField] private float speed;

        [SerializeField] private float lifeTime = 3;
        [SerializeField] private int damage;
        private Vector3 _move;

        private int _blockingLayer;

        private void Awake()
        {
            _blockingLayer = LayerMask.NameToLayer("Level");
        }

        void Start()
        {
            Invoke("Kill", lifeTime);
        }
    
        void FixedUpdate()
        {
            transform.Translate(Time.deltaTime*speed*Vector3.forward);
        }
    
        void OnTriggerEnter(Collider other)
        {
            GameObject g = other.GameObject();
            if (g.layer == _blockingLayer)
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
            Instantiate(hitVisual, transform1.position, transform1.rotation);
            Destroy(gameObject);
        }
    }
}
