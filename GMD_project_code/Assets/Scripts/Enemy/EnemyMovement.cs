using System;
using GMDProject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour, ICharacterMovement
    {
        [SerializeField] private float maxRotateYSpeed;

        [SerializeField] private float speed;
        [SerializeField] private float maxVelocity;
        
        private Rigidbody _rb;
        
        private float _rotationAngleY;

        private bool _hasTarget;
        private Vector3 _targetPoint;

        private bool _moveForward;

        private void Awake()
        {

            _rb = GetComponent<Rigidbody>();
        }

        public void Jump()
        {
            throw new System.NotImplementedException();
        }

        public void MoveRight()
        {
            throw new System.NotImplementedException();
        }

        public void MoveLeft()
        {
            throw new System.NotImplementedException();
        }

        public void MoveForward()
        {
            _moveForward = true;
        }

        public void MoveBackward()
        {
            throw new System.NotImplementedException();
        }

        public void RotateX(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void RotateY(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void RotateTowards(Vector3 point)
        {
            _hasTarget = true;
            _targetPoint = point;
        }

        public void RotateZ(float amount)
        {
            throw new System.NotImplementedException();
        }

        private void FixedUpdate()
        {
            if (_hasTarget)
            {
                Transform transform1 = transform;
                Vector3 targetDirection = _targetPoint - transform1.position;


                Vector3 flatTargetDirection = targetDirection;
                flatTargetDirection.y = 0;

                Vector3 flatFacedDirection = transform1.forward;
                flatFacedDirection.y = 0;

                float absAngleYToTarget = Vector3.Angle(flatTargetDirection, flatFacedDirection);

                
                if (!Mathf.Approximately(absAngleYToTarget, 0))
                { 
                    float turnDirection = Mathf.Sign(Vector3.Cross(flatFacedDirection, flatTargetDirection).y);
                    float angle = absAngleYToTarget * turnDirection;
                    float maxRotationStep = maxRotateYSpeed * Time.deltaTime;
                    _rotationAngleY = absAngleYToTarget > maxRotationStep ? maxRotationStep * turnDirection : angle;
                    _rotationAngleY /= 2; // ???
                    Quaternion q = Quaternion.Euler(0, _rotationAngleY, 0);

                    _rb.MoveRotation(transform.rotation * q);
                }

                _hasTarget = false;
            }

            if (_moveForward)
            {
                if (_rb.velocity.magnitude < maxVelocity)
                {
                    _rb.AddForce(transform.forward * (speed*Time.deltaTime), ForceMode.VelocityChange);
                }
            }
        }
    }
}