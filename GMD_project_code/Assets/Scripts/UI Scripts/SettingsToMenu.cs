using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsToMenu : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
