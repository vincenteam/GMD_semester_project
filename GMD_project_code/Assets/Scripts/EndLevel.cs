using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceEnd;
    [SerializeField] private string sceneName;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerInput>() != null)
        {
            StartCoroutine(SwitchScene(sceneName));
        }
    }

    private IEnumerator SwitchScene(string scene)
    {
        audioSourceEnd.Play();
        yield return new WaitForSecondsRealtime((float)1.5);
        TransitionManager.transitionInstance.ExitToMenu();
    }
}
