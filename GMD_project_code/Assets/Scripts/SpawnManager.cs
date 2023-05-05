using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceSpawn;

    public GameObject Spawn(GameObject go)
    {
        return Instantiate(go, transform.position, transform.rotation);
    }

    public void StartSpawnMusic()
    {
        audioSourceSpawn.Play();
    }
}
