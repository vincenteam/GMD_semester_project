using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace GMDProject
{
    public class UpDownDoor : MonoBehaviour, IDoor
    {
        [SerializeField] private float speed;
        
        private Rigidbody _rb;
        private Vector3 _targetPoint;
        private Vector3 _startPoint;
        
        private bool opening = false;
        private bool closing = false;
        private bool open = false;

        void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            _startPoint = _rb.position;
            _targetPoint = transform.position;

            Collider col = gameObject.GetComponent<Collider>();
            _targetPoint.y += col.bounds.size.y;
        }

        public void Open()
        {
            if (!opening && !open)
            {
                StartCoroutine(MoveTo(_targetPoint));
                opening = true;
            }
        }

        private IEnumerator MoveTo(Vector3 target)
        {
            Vector3 direction = target - _rb.position;
            float distance = direction.magnitude;
            direction = Vector3.Normalize(direction);
            float distanceDone = 0;

            while (distanceDone < distance)
            {
                var step = speed * Time.deltaTime * direction;
                _rb.position += step;
                distanceDone += speed * Time.deltaTime;

                yield return null;
            }
            
            _rb.position = target;
            
            opening = false;
            open = true;
        }

        public void Close()
        {
            if (open && !closing)
            {
                StartCoroutine(MoveTo(_startPoint));
                closing = true;
            }
        }
    }
}