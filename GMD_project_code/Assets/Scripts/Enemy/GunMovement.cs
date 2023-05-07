using System;
using UnityEngine;

namespace Enemy
{
    public class GunMovement : MonoBehaviour
    {
        [SerializeField] private float maxRotateSpeed;
        private bool _hasTarget;
        private Vector3 _targetPoint;

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

                flatTargetDirection.z = Mathf.Abs(flatTargetDirection.z);
                flatFacedDirection.z = Mathf.Abs(flatFacedDirection.z);
                
                if (!Mathf.Approximately(absAngleXToTarget, 0))
                { 
                    float turnDirection = Mathf.Sign(Vector3.Cross(flatFacedDirection, flatTargetDirection).x);
                    print("cross " + Vector3.Cross(flatFacedDirection, flatTargetDirection));
                    float angle = absAngleXToTarget * turnDirection;
                    float maxRotationStep = maxRotateSpeed * Time.deltaTime;
                    float rotationAngleX = absAngleXToTarget > maxRotationStep ? maxRotationStep * turnDirection : angle;
                    rotationAngleX /= 2; // ???
                    print("step " + rotationAngleX);
                    print("angle " + absAngleXToTarget);
                    Quaternion q = Quaternion.Euler(rotationAngleX, 0, 0);
                    
                    transform.rotation *= q;
                }
                
                _hasTarget = false;
            }
        }
    }
}