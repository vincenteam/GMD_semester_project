using System.Collections;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceEnd;
    [SerializeField] private string sceneName;
    private void OnTriggerEnter(Collider other)
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
        TransitionManager.TransitionInstance.Transition(scene);
    }
}