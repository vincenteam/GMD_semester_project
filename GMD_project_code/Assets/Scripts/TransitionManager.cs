using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager TransitionInstance;
    void Awake(){
        DontDestroyOnLoad (this);
         
        if (TransitionInstance == null) {
            TransitionInstance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public void Transition(string sceneName)
    {
        /*if (SceneManager.GetActiveScene().name == sceneName)
        {
            //If the user resets the current scene and doesn't go to another one, the background music somehow needs to persist
            SceneManager.LoadScene(sceneName);
        }
        else
        {*/
        if (sceneName == "TitleScreen")
        {
            SceneManager.LoadScene("TitleScreen");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        //}
    }
}
