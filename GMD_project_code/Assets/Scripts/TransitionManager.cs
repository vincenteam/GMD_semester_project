using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager transitionInstance;
    void Awake(){
        DontDestroyOnLoad (this);
         
        if (transitionInstance == null) {
            transitionInstance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public void Transition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
