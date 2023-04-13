using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonTest : MonoBehaviour, ButtonInterface
{
    public void React()
    {
        Debug.Log("Button activated");
    }
}
