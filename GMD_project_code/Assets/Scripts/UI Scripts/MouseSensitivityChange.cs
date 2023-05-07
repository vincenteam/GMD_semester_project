using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityChange : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;

    private void Start()
    {
        //ATTENTION : This code can be replaced with just a Load() but is kept for the development
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetFloat("Sensitivity", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeSensitivity()
    {
        Save(sensitivitySlider.value);
    }

    private void Load()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }

    private void Save(float sliderValue)
    {
        PlayerPrefs.SetFloat("Sensitivity", sliderValue);
    }
}
