using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToLevel1 : MonoBehaviour
{
    [SerializeField] private AudioSource menuMusic;
    public void ChangeScene()
    {
        menuMusic.Pause();
        SceneManager.LoadScene("tests");
    }
}
