using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardTypeChange : MonoBehaviour
{
    [SerializeField] private TMP_Text display;
    public void AzertyClicked()
    {
        display.text = "Current keyboard type : Azerty";
        Save("Azerty");
    }
    
    public void QwertyClicked()
    {
        display.text = "Current keyboard type : Qwerty";
        Save("Qwerty");
    }

    private void Save(string keyboardType)
    {
        if (keyboardType == "Azerty")
        {
            PlayerPrefs.SetString("Forward", "Forward");
            PlayerPrefs.SetString("RightLeft", "RightLeft");
            PlayerPrefs.SetString("Display", "Current keyboard type : Azerty");
        }
        if (keyboardType == "Qwerty")
        {
            PlayerPrefs.SetString("Forward", "ForwardQwerty");
            PlayerPrefs.SetString("RightLeft", "RightLeftQwerty");
            PlayerPrefs.SetString("Display", "Current keyboard type : Qwerty");
        }
    }
}
