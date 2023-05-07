using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefaultPlayerPrefs: MonoBehaviour
{
    private void Awake()
    {
        //Save the defaults Azerty keyboard inputs if there isn't any PlayerPref for it
        if (!PlayerPrefs.HasKey("Forward"))
        {
            PlayerPrefs.SetString("Forward", "Forward");
        }
        if (!PlayerPrefs.HasKey("RightLeft"))
        {
            PlayerPrefs.SetString("RightLeft", "RightLeft");
        }
        
        //Save the default sensitivity value if there isn't any PlayerPref for it
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetFloat("Sensitivity", 1);
        }
    }
}