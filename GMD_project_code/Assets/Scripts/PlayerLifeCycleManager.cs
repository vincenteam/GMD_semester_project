using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLifeCycleManager : MonoBehaviour
{
    private SpawnManager _spawn;
    private FollowPlayer _cam;
    [SerializeField] private GameObject player;
    private GameObject newPlayer;
    
    void Start()
    {
        GameObject spawnGo = GameObject.Find("PlayerSpawn");
        if (spawnGo) _spawn = spawnGo.GetComponent<SpawnManager>();
        
        GameObject cam = GameObject.Find("Main Camera");
        if (cam) _cam = cam.GetComponent<FollowPlayer>();

        OnDeath(); //tmp ?
        EnablePlayer();
    }

    public void OnDeath()
    {
        if (_spawn)
        {
             newPlayer = _spawn.Spawn(player);
             newPlayer.SetActive(false);
             
             Alive playerLife = newPlayer.GetComponent<Alive>();
             if (playerLife is not null)
             {
                 playerLife.OnDeath += OnDeath;
                 playerLife.OnDeathActionsExit += EnablePlayer;
             }
             
             _cam.ChangeFollowTarget(newPlayer);
        }
    }

    private void EnablePlayer()
    {
        newPlayer.SetActive(true);
    }
}
