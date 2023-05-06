using System;
using GMDProject;
using UnityEngine;
namespace Enemy
{
    public class DestroyParticles : MonoBehaviour
    {
        private void Start()
        {
            Invoke( nameof(DestroyParticlesOnEnd),2);
        }

        private void DestroyParticlesOnEnd()
        {
            Destroy(gameObject);
        }
    }
}