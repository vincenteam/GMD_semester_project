using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefaultDisplayKeyboardType : MonoBehaviour
{
    [SerializeField] private TMP_Text display;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Display"))
        {
            PlayerPrefs.SetString("Display", "Current keyboard type : Azerty");
            display.text = PlayerPrefs.GetString("Display");
        }
        else
        {
            display.text = "Current keyboard type : Azerty";
        }
    }
}
