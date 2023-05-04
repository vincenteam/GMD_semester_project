using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class SniperActions : MonoBehaviour
    {
        [SerializeField] private int lookDistance;
        [SerializeField] string[] targetLayers = new[] { "Living" };
        [SerializeField] string[] obstacleLayers = new[] { "Level", "Objects" };

        private Loadout _loadout;
        private Coroutine _currentState;
        private int _targetsLayersMask;
        private int _obstacleLayersMask;
        
        private int _maxColliders = 100;
        
        
        private void Awake()
        {
            _loadout = GetComponent<Loadout>();

            _targetsLayersMask = LayerMask.GetMask(targetLayers);
            _obstacleLayersMask = LayerMask.GetMask(obstacleLayers);
        }

        void Start()
        {
            
        }
        
        void OnEnable()
        {
            _currentState = StartCoroutine(nameof(CheckForTarget));
            
        }

        public GameObject CheckForTarget()
        {
            Collider[] inRange = new Collider[_maxColliders];
            var nbInRange = Physics.OverlapSphereNonAlloc(transform.position, lookDistance, inRange, _targetsLayersMask);
            for (int i = 0; i < nbInRange; i++)
            {
                Rigidbody rb = inRange[i].attachedRigidbody;
                GameObject go;
                if (rb != null)
                {
                    go = rb.gameObject;
                    if (rb.gameObject.CompareTag("Friendly") && CanSee(go))
                    {
                        return go;
                    }
                }
            }

            return null;
        }

        public bool CanSee(GameObject target)
        {
            Collider[] colls = target.GetComponentsInChildren<Collider>();
            var position = target.transform.position;
            float maxTop = position.y;
            float maxBottom = position.y;
            float maxRight = position.x;
            float maxLeft = position.x;
            float maxFront = position.z;
            float maxBack = position.z;

            foreach (var col in colls)
            {
                var bounds = col.bounds;
                var boundsMax = bounds.max;
                var boundsMin = bounds.min;

                float top = boundsMax.y;
                float bottom = boundsMin.y;
                float right = boundsMin.x;
                float left = boundsMax.x;
                float front = boundsMax.z;
                float back = boundsMin.z;

                if (top > maxTop) maxTop = top;
                if (bottom < maxBottom) maxBottom = bottom;
                if (right < maxRight) maxRight = right;
                if (left > maxLeft) maxLeft = left;
                if (front > maxFront) maxFront = front;
                if (back < maxBack) maxBack = back;

            }

            Vector3[] extremities = new[]
            {
                new Vector3(position.x, maxTop, position.z),
                new Vector3(position.x, maxBottom, position.z),
                new Vector3(maxRight, position.y, position.z),
                new Vector3(maxLeft, position.y, position.z),
                new Vector3(position.x, position.y, maxBack),
                new Vector3(position.x, position.y, maxFront)
            };

            foreach (var point in extremities)
            {
                var origin = transform.position;
                Vector3 direction = (point - origin);

                bool intersect = Physics.Raycast(origin, direction, direction.magnitude,
                    _obstacleLayersMask, QueryTriggerInteraction.UseGlobal);

                //if (intersect) Debug.DrawRay(origin, direction, Color.blue, 0.5f);

                if (!intersect)
                {
                    Debug.DrawRay(origin, direction, Color.red, 0.5f);
                    return true;
                }
            }

            return false;
        }


        private IEnumerator AimAt(GameObject target)
        {
            print("aim at " + target);
            yield return new WaitForSeconds(1f);
            _currentState = StartCoroutine(nameof(CheckForTarget));
            yield break;
        }
        
        
        private IEnumerator IdleShoot()
        {
            while (true)
            {
                _loadout.Gun.Fire();
                yield return new WaitForSeconds(1f);   
            }
        }
    }
}
