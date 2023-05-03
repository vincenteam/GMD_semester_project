using UnityEngine;

public class PlayerLifeCycleManager : MonoBehaviour
{
    private SpawnManager _spawn;
    private FollowPlayer _cam;
    private SwitchPosition _camView;
    [SerializeField] private GameObject player;
    private GameObject _newPlayer;
    
    void Start()
    {
        GameObject spawnGo = GameObject.Find("PlayerSpawn");
        if (spawnGo) _spawn = spawnGo.GetComponent<SpawnManager>();
        
        GameObject cam = GameObject.Find("Main Camera");
        if (cam)
        {
            _cam = cam.GetComponent<FollowPlayer>();
            _camView = cam.GetComponent<SwitchPosition>();
        }

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
                 playerLife.OnDeath += OnDeath;
                 playerLife.OnDeath += delegate { _camView.SwitchTo("3person"); };
                 playerLife.OnDeathActionsExit += EnablePlayer;
                 playerLife.OnDeathActionsExit += _cam.UnLock;
             }

             _cam.ChangeFollowTarget(_newPlayer);
        }
    }

    private void EnablePlayer()
    {
        _newPlayer.SetActive(true);
        _spawn.StartSpawnMusic();
    }
}
