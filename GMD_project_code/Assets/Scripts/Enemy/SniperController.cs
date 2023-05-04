using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class SniperController: MonoBehaviour
    {
        [SerializeField] private float checkInterval;
        private SniperActions _actions;

        public delegate void ActionDelegate();

        public ActionDelegate OnVigilantStart;
        public ActionDelegate OnVigilantEnd;
        public ActionDelegate OnAlertedStart;
        public ActionDelegate OnAlertedEnd;
        
        private void Awake()
        {
            _actions = gameObject.GetComponent<SniperActions>();
            OnAlertedEnd += delegate { StartCoroutine(nameof(Vigilant)); };
        }
        
        void OnEnable()
        {
            StartCoroutine(nameof(Vigilant));
        }

        public IEnumerator Vigilant()
        {
            if (OnVigilantStart != null) OnVigilantStart();

            while (true)
            {
                GameObject target = _actions.CheckForTarget();
                if (target != null)
                {   
                    print("aim to " + target);
                    StartCoroutine(Alerted(target));
                    break;
                }
                yield return new WaitForSeconds(checkInterval);
            }
            
            if (OnVigilantEnd != null) OnVigilantEnd();
        }

        public IEnumerator Alerted(GameObject target)
        {
            if (OnAlertedStart != null) OnAlertedStart();
            
            Coroutine tracking = StartCoroutine(_actions.Track(target));
            Coroutine shooting = StartCoroutine(_actions.DumbShoot());
            while (true)
            {
                if (target == null || !_actions.CanSee(target))
                {
                    StopCoroutine(tracking);
                    StopCoroutine(shooting);
                    break;
                }
                yield return new WaitForSeconds(checkInterval);
            }
            
            if (OnAlertedEnd != null) OnAlertedEnd();
        }
    }
}
