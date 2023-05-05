using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.Scene;

public class MenuToLevel1 : MonoBehaviour
{
    //private AudioSource _menuMusic;
    private GameObject _temp;
    public void ChangeScene()
    {
        _temp = GameObject.FindGameObjectWithTag("BackgroundMusic");
        Destroy(_temp);
        SceneManager.LoadScene("tests");
    }
}
