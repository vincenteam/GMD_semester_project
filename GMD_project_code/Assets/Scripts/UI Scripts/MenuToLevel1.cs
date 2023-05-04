using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToLevel1 : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("tests");
    }
}
