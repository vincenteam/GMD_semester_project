using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GMDProject
{
    public class UpDownDoor : MonoBehaviour, IDoor
    {
        private Rigidbody _rb;
        private Vector3 _targetPoint;

        void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            _targetPoint = transform.position;

            Collider col = gameObject.GetComponent<Collider>();
            _targetPoint.y += col.bounds.size.y/2;
        }
        
        public void Open()
        {
            //StartCoroutine(MoveTo(_targetPoint));
        }

        /*private IEnumerator MoveTo(Vector3 target)
        {
            return ;
        }*/

        public void Close()
        {
            
        }
    }
}