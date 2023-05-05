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
        SceneManager.LoadScene(sceneName);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
