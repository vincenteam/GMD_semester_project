using UnityEngine;

namespace Enemy
{
    public class RunnerController : MonoBehaviour
    {
        private RunnerActions _actions;
        private Guarding _guarding;
        private Alive _life;

        private void Awake()
        {
            _actions = gameObject.GetComponent<RunnerActions>();
            _guarding = gameObject.GetComponent<Guarding>();
            _life = GetComponent<Alive>();
            
            _guarding.OnVigilantEnd += delegate (GameObject target){ StartCoroutine(_guarding.Alerted(target)); };
            _guarding.OnAlertedEnd += delegate
            {
                _actions.Crashing = false;
                StartCoroutine(_guarding.Vigilant());
            };
            
            _guarding.OnAlertedStart += delegate (GameObject target)
            {
                _actions.Crashing = true;
                StartCoroutine(_actions.Track(target));
                StartCoroutine(_actions.Run());
            };
            _guarding.OnAlertedEnd += StopAllCoroutines;
            
            
            _life.Terminate = Destroy;
        }
    }
}
