using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class SniperController: MonoBehaviour
    {
        [SerializeField] private float checkInterval;
        private SniperActions actions;

        private void Awake()
        {
            actions = gameObject.GetComponent<SniperActions>();
        }


        void OnEnable()
        {
            StartCoroutine(nameof(Vigilant));
        }

        public IEnumerator Vigilant()
        {
            while (true)
            {
                GameObject target = actions.CheckForTarget();
                if (target != null)
                {   
                    print("aim to " + target);
                }
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }
}