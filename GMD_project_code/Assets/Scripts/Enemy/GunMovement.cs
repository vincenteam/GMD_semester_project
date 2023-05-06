using System;
using UnityEngine;

namespace Enemy
{
    public class GunMovement : MonoBehaviour
    {
        [SerializeField] private float maxRotateSpeed;
        private bool _hasTarget;
        private Vector3 _targetPoint;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void AimTowardsX(Vector3 point)
        {
            _hasTarget = true;
            _targetPoint = point;
        }

        public void FixedUpdate()
        {
            if (_hasTarget)
            {
                Transform transform1 = transform;
                Vector3 targetDirection = _targetPoint - transform1.position;
                
                Vector3 flatTargetDirection = targetDirection;
                flatTargetDirection.x = 0;

                Vector3 flatFacedDirection = transform1.forward;
                flatFacedDirection.x = 0;

                float absAngleXToTarget = Vector3.Angle(flatTargetDirection, flatFacedDirection);
                print("angle " + absAngleXToTarget);
                if (!Mathf.Approximately(absAngleXToTarget, 0))
                { 
                    float turnDirection = Mathf.Sign(Vector3.Cross(flatFacedDirection, flatTargetDirection).x);
                    float angle = absAngleXToTarget * turnDirection;
                    float maxRotationStep = maxRotateSpeed * Time.deltaTime;
                    float rotationAngleX = absAngleXToTarget > maxRotationStep ? maxRotationStep * turnDirection : angle;
                    rotationAngleX /= 2; // ???
                    Quaternion q = Quaternion.Euler(rotationAngleX, 0, 0);

                    transform.rotation = transform.rotation * q;
                }
                
                _hasTarget = false;
            }
        }
    }
}