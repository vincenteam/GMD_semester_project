using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RunnerActions : MonoBehaviour
    {
        [SerializeField] string[] obstacleLayers = new[] { "Level", "Objects", "Living"};
        private int _obstacleLayersMask;
        private int _livingLayer;
        
        [SerializeField] private Collider sensitiveCollider;
        
        private EnemyMovement _movement;
        private Alive _life;

        private bool _crashing;

        public bool Crashing
        {
            get => _crashing;
            set => _crashing = value;
        }
        
        private void Awake()
        {
            _movement = GetComponent<EnemyMovement>();
            _life = GetComponent<Alive>();
            
            _obstacleLayersMask = LayerMask.GetMask(obstacleLayers);
            _livingLayer = LayerMask.NameToLayer("Living");
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

        public IEnumerator Run()
        {
            while (true)
            {
                _movement.MoveForward();

                yield return null;
            }
        }

       private void OnCollisionEnter(Collision other)
       {
           print("collision");
            if (_crashing && other.contacts[0].thisCollider == sensitiveCollider)
            {
                print("collision " + other.contacts[0].thisCollider.gameObject.name);
                GameObject go = other.gameObject;
                int layer = go.layer;
                if ((1<<layer & _obstacleLayersMask) != 0)
                {
                    if (!(layer == _livingLayer && !go.CompareTag("Friendly")))// non friendly Living objects are ignored
                    {
                        _life.Die(DamageTypes.Explode);
                        _life.TerminateDeath(DamageTypes.Explode);
                    }
                }
            }
        }
    }
}
