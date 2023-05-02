using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAudioBetweenScenes : MonoBehaviour
{
  private void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }
}
