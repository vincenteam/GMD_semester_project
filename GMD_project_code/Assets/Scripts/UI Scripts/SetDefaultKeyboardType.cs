using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefaultKeyboardType: MonoBehaviour
{
    private void Awake()
    {
        //Save the defaults Azerty keyboard inputs if there isn't any preferencies set
        if (!PlayerPrefs.HasKey("Forward"))
        {
            PlayerPrefs.SetString("Forward", "Forward");
        }
        if (!PlayerPrefs.HasKey("RightLeft"))
        {
            PlayerPrefs.SetString("RightLeft", "RightLeft");
        }
    }
}