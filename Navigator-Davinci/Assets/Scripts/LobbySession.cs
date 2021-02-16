using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySession : MonoBehaviour
{
    [SerializeField] private Scene lobbyScene;

    [SerializeField] private GameObject lobby;

    [SerializeField] private Player prefab;

    [SerializeField] private List<Player> players;

    public static LobbySession instance;

    // Update is called once per frame
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
        Player plr = Instantiate(prefab);
        players.Add(plr);
    }

    public void DespawnPlayer(Player plr)
    {
        players.Remove(plr);
        Destroy(plr.gameObject);
    }
}
