using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeActions : MonoBehaviour
{
    [SerializeField] private Alive lifeEvents;
    [SerializeField] private GameObject body; // prefab

    // Start is called before the first frame update
    void Start()
    {
        lifeEvents.OnDeath += OnDeath;
        lifeEvents.Terminate = Destroy;
    }

    private void OnDeath()
    {
        
        Instantiate(body, transform.position, transform.rotation);
        // animation
        //...
        Invoke("TerminateDeath", 1);
    }

    private void TerminateDeath()
    {        
        GameObject cam_go = Tools.GetTransformByTag(transform, "MainCamera").gameObject;
         if (cam_go)
         {
             FollowPlayer cam = cam_go.GetComponent<FollowPlayer>();
             if(cam) cam.Unlock();
         }
        lifeEvents.TerminateDeath();
    }
}
