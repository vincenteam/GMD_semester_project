using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelItems
{
    public class ButtonPressed : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> scriptsTarget = new();
        [SerializeField] private List<string> methodTarget = new();
        [SerializeField] private List<string> eventTarget = new();

        public delegate void ActionDelegate();
        private ActionDelegate _buttonDown;
        private ActionDelegate _buttonUp;

        private bool _isPressed;
        public bool IsPressed
        {
            get => _isPressed;
        }

        private int _collisionCount;
    
        [SerializeField] private AudioSource audioSourcePressed;

        public ActionDelegate OnButtonDown
        {
            get => _buttonDown;
            set => _buttonDown += value;
        }

        private void Awake()
        {
            foreach ((MonoBehaviour, string, string) kvp in scriptsTarget.Zip(methodTarget, (script, str) => (script, str)).Zip(eventTarget, (scriptMethode, eventName) => (scriptMethode.Item1, scriptMethode.Item2, eventName)))
            {
                switch (kvp.Item3)
                {
                    case "ButtonDown":
                        _buttonDown += (ActionDelegate)Delegate.CreateDelegate(typeof(ActionDelegate), kvp.Item1, kvp.Item2, true, false);
                        break;
                    case "ButtonUp":
                        _buttonUp += (ActionDelegate)Delegate.CreateDelegate(typeof(ActionDelegate), kvp.Item1, kvp.Item2, true, false);
                        break;
                    default:
                        print("event " + kvp.Item3 + " does not exist in class " + typeof(ButtonPressed));
                        break;
                }
            }
        }

        private void Pressed()
        {
            _isPressed = true;
            audioSourcePressed.Play();
            _buttonDown();
        }
    
        void OnCollisionEnter(Collision collision)
        {
            _collisionCount++;
            if (!_isPressed && _buttonDown != null)
            {
//                print("button down");
                Pressed();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            _collisionCount--;
            if (_collisionCount <= 0 && _buttonUp != null)
            {
 //               print("button up");
                _isPressed = false;
                _buttonUp();
            }
        }
    }
}
