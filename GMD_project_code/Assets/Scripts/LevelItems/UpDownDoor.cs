using System.Collections;
using UnityEngine;

namespace LevelItems
{
    public class UpDownDoor : MonoBehaviour, IDoor
    {
        [SerializeField] private float speed;
        
        private Rigidbody _rb;
        private Vector3 _targetPoint;
        private Vector3 _startPoint;
        
        private bool _opening = false;
        private bool _closing = false;
        private bool _open = false;

        private Coroutine _movement;

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
            if (_closing || !_open)
            {
                if(_movement != null) StopCoroutine(_movement);
                _movement = StartCoroutine(MoveTo(_targetPoint));
                _opening = true;
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

            _opening = false;
            _open = !_open;
        }

        public void Close()
        {
            print(_opening + " " + _open);
            if (_opening || _open)
            {
                print("close");
                if(_movement != null) StopCoroutine(_movement);
                _movement = StartCoroutine(MoveTo(_startPoint));
                _closing = true;
            }
        }
    }
}
