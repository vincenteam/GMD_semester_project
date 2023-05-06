using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTypeChange : MonoBehaviour
{
    public void AzertyClicked()
    {
        Save("Azerty");
    }
    
    public void QwertyClicked()
    {
        Save("Qwerty");
    }

    private void Save(string keyboardType)
    {
        if (keyboardType == "Azerty")
        {
            PlayerPrefs.SetString("Forward", "Forward");
            PlayerPrefs.SetString("RightLeft", "RightLeft");
        }
        if (keyboardType == "Qwerty")
        {
            PlayerPrefs.SetString("Forward", "ForwardQwerty");
            PlayerPrefs.SetString("RightLeft", "RightLeftQwerty");
        }
    }
}
