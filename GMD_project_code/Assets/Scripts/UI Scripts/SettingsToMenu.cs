using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsToMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void ChangeScene()
    {
        TransitionManager.TransitionInstance.Transition(sceneName);
    }
}
