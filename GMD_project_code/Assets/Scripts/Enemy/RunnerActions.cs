using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RunnerActions : MonoBehaviour
    {
        [SerializeField] string[] obstacleLayers = new[] { "Level", "Objects", "Living"};
        private int _obstacleLayersMask;
        private int _livingLayer;
        
        private EnemyMovement _movement;
        private Alive _life;

        private bool _crashing;
        [SerializeField] private Collider sensitiveCollider; 

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

       private void OnTriggerEnter(Collider other)
        {
            if (_crashing )
            {
                GameObject go = other.gameObject;
                int layer = go.layer;
                if ((1<<layer & _obstacleLayersMask) != 0)
                {
                    if (!(layer == _livingLayer && go.CompareTag("Friendly")))// non friendly Living objects are ignored
                    {
                        _life.Die(DamageTypes.Explode);
                        _life.TerminateDeath(DamageTypes.Explode);
                    }
                }
            }
        }
    }
}
