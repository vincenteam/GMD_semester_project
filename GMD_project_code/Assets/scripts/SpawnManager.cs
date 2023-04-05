using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Spawn(GameObject go)
    {
        return Instantiate(go, transform.position, transform.rotation);
    }
}
