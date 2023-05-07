using System;
using System.Collections;
using UnityEngine;

namespace LevelItems
{
    public class ConsumeBodies : MonoBehaviour
    {
        [SerializeField] private float consumeRate;
        [SerializeField] private string bodiesTag;
        [SerializeField] private float centerAttraction = 0; // how fast bodies are attracted towards the center of the grinder
        private Vector3 _center;

        private void Awake()
        {
            Collider col = gameObject.GetComponent<Collider>();
            _center = new Vector3(0, col.bounds.size.y/2, 0);
            //print("center " + _center);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(bodiesTag))
            {
                //print("start running");
                collision.rigidbody.isKinematic = true;
                StartCoroutine(ConsumeBody(collision.collider));
            }
        }

        private IEnumerator ConsumeBody(Collider col)
        {
            var bounds = col.bounds;
            //print("bounds " + bounds.size.y);
            Vector3 targetPoint = _center;
            float targetPointYShift = transform.InverseTransformPoint(col.transform.position).y;

            float integralAccumulator = 0;
            float elapsedTime = 0;

            float size = bounds.size.y;
            float consumed = 0;
            while (consumed < size)
            {
                if (col == null)
                {
                    yield return null;
                }
                elapsedTime += Time.deltaTime*consumeRate;
                float linearFunc = centerAttraction * (elapsedTime + 1) + 1f;
                integralAccumulator += linearFunc*Time.deltaTime*consumeRate;
                targetPoint.y = targetPointYShift - (integralAccumulator);
                //print( "linear " + linearFunc + " target point " + targetPoint.y);
            
                Vector3 direction = targetPoint - transform.InverseTransformPoint(col.transform.position);
                //print(direction);
                direction = Vector3.ClampMagnitude(direction, 1);
                direction = transform.TransformDirection(direction);
            
                float step = consumeRate * Time.deltaTime;
                Vector3 move = step * direction;
                col.transform.Translate(move, Space.World); // moveposition
                consumed += Math.Abs(move.y);
            
                yield return null;
            }
            //print("coroutine end");
            Destroy(col.gameObject);
        }
    }
}
