using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class SniperController: MonoBehaviour
    {
        [SerializeField] private float checkInterval;
        private SniperActions _actions;

        private void Awake()
        {
            _actions = gameObject.GetComponent<SniperActions>();
        }


        void OnEnable()
        {
            StartCoroutine(nameof(Vigilant));
        }

        public IEnumerator Vigilant()
        {
            while (true)
            {
                GameObject target = _actions.CheckForTarget();
                if (target != null)
                {   
                    print("aim to " + target);
                    StartCoroutine(Alerted(target));
                    yield break;
                }
                yield return new WaitForSeconds(checkInterval);
            }
        }

        public IEnumerator Alerted(GameObject target)
        {
            Coroutine tracking = StartCoroutine(_actions.Track(target));

            while (true)
            {
                if (!_actions.CanSee(target))
                {
                    StopCoroutine(tracking);
                    yield break;
                }
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }
}
