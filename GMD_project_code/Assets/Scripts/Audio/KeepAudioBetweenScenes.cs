using UnityEngine;

public class KeepAudioBetweenScenes : MonoBehaviour
{
  private void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
  }
}
