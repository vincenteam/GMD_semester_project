using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class SniperController: MonoBehaviour
    {
        private SniperActions _actions;
        private Guarding _guarding;

        private void Awake()
        {
            _actions = gameObject.GetComponent<SniperActions>();
            _guarding = gameObject.GetComponent<Guarding>();
            
            _guarding.OnVigilantEnd += delegate (GameObject target){ StartCoroutine(_guarding.Alerted(target)); };
            _guarding.OnAlertedEnd += delegate
            {
                StopAllCoroutines();
                StartCoroutine(_guarding.Vigilant());
            };
            
            
            _guarding.OnAlertedStart += delegate (GameObject target)
            {
                StartCoroutine(_actions.Track(target));
                StartCoroutine(_actions.DumbShoot());
            };
            
        }
    }
}
