using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSession : MonoBehaviour
{
    [SerializeField] private Scene lobbyScene;

    [SerializeField] private GameObject room;

    [SerializeField] private Player prefab;

    [SerializeField] private List<Player> players;

    public static RoomSession instance;

    private void Start()
    {
        instance = this;
        SpawnPlayer();
    }
    void Update()
    {
    }

    public void SpawnPlayer()
    {
        print("printen");
        Player plr = Instantiate(prefab);
        players.Add(plr);
    }

    public void DespawnPlayer(Player plr)
    {
        players.Remove(plr);
        Destroy(plr.gameObject);
    }


}
