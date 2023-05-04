using System.Collections;
using UnityEngine;

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
                    StartCoroutine(actions.Track(target));
                    yield return null;
                }
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }
}