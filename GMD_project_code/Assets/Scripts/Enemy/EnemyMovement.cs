using GMDProject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour, ICharacterMovement
    {
        [FormerlySerializedAs("rotateYSpeed")] [SerializeField] private float maxRotateYSpeed;

        private Rigidbody _rb;
        
        private float _rotationYAccumulator;
        private float _rotationAngleY;

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
            throw new System.NotImplementedException();
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
            _rotationYAccumulator += amount;
        }

        public void RotateZ(float amount)
        {
            throw new System.NotImplementedException();
        }

        private void FixedUpdate()
        {
            float maxRotationStep = maxRotateYSpeed * Time.deltaTime;
            _rotationAngleY = _rotationYAccumulator > maxRotationStep ? maxRotationStep : _rotationYAccumulator;
            
            Quaternion q = Quaternion.Euler(0, _rotationAngleY, 0);
            _rb.MoveRotation(transform.rotation * q);

            _rotationYAccumulator -= _rotationAngleY;
        }
    }
}