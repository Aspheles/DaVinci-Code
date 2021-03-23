using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSession : Environment
{
    public static RoomSession instance;
    private void Start()
    {
        ResetPosition();
    }

    public void ResetPosition()
    {
        SpawnPlayer(spawnPoint);
    }
}
