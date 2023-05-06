using Cinemachine;
using UnityEngine;

public class PlayerLifeCycleManager : MonoBehaviour
{
    private SpawnManager _spawn;
    [SerializeField] private GameObject player;
    private GameObject _newPlayer;

    private CinemachineVirtualCamera _vCam;
    
    void Start()
    {
        GameObject spawnGo = GameObject.Find("PlayerSpawn");
        if (spawnGo) _spawn = spawnGo.GetComponent<SpawnManager>();

        GameObject cam = GameObject.FindGameObjectWithTag("VirtualCamera");
        if (cam != null) _vCam = cam.GetComponent<CinemachineVirtualCamera>();
        
        OnDeath(); //tmp ?
        EnablePlayer();
    }

    public void OnDeath()
    {
        if (_spawn)
        {
            _newPlayer = _spawn.Spawn(player); 
            _newPlayer.SetActive(false);
            
             
            Alive playerLife = _newPlayer.GetComponent<Alive>();
            if (playerLife is not null)
            {
                playerLife.OnDeath += _ => { OnDeath(); };
                playerLife.OnDeathDeathExit += _ => { EnablePlayer(); };
            }
        }
    }

    private void EnablePlayer()
    {
        _vCam.Follow = _newPlayer.transform;
        _vCam.LookAt = _newPlayer.GetComponentInChildren<HeadMovement>().gameObject.transform;
        
        _newPlayer.SetActive(true);
        _spawn.StartSpawnMusic();
    }
}
