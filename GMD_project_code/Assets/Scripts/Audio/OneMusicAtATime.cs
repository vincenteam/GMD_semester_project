using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneMusicAtATime : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("BackgroundMusic").Length > 1)
        {
            Destroy(this.GameObject());
        };
    }
}
