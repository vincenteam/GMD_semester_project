using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToSettings : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Settings");
    }
}
