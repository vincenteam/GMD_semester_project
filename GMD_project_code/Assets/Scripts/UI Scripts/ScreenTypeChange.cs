using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class ScreenTypeChange : MonoBehaviour
{
    public void  TypeChange()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
