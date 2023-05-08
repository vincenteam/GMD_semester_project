using System.Collections;
using System.Collections.Generic;
using LevelItems;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class TitleSceneBackGroundManager : MonoBehaviour
{
    [SerializeField]
    private UpDownDoor roombaDoor;

    [SerializeField]
    private SpawnManager playerSpawn;
    [SerializeField]
    private SpawnManager roombaSpawn;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject roomba;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCycle();
    }

    private void StartCycle()
    {
        print("start cycle");
        roombaDoor.Close();
        SetUpPlayer();
        SetUpRoomba();
    }

    private void SetUpPlayer()
    {
        GameObject p = playerSpawn.Spawn(player);
        Alive life = p.GetComponentInChildren<Alive>();
        life.OnDeathDeathExit +=_ =>  SetUpPlayer();
        p.GetComponentInChildren<PlayerInput>().enabled = false;
    }

    private void SetUpRoomba()
    {
        roombaDoor.Close();
        GameObject r = roombaSpawn.Spawn(roomba);
        Alive roombaLife = r.GetComponentInChildren<Alive>();
        roombaLife.OnDeathDeathExit += _ => { SetUpRoomba(); };
        Invoke(nameof(OpenDoor), Random.Range(10, 30));
    }

    private void OpenDoor()
    {
        roombaDoor.Open();
    }
}
