using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour
{

    [SerializeField] private Scene scene;

    [SerializeField] private GameObject environment;

    [SerializeField] private Player playerPrefab;

    [SerializeField] public GameObject spawnPoint;

    [SerializeField] private List<Player> players;

    [SerializeField] public List<Portal> portals;

    private void Start()
    {
        scene = SceneManager.GetSceneByName(environment.name);
    }
    public void SpawnPlayer(GameObject spawn)
    {
        Player plr = Instantiate(playerPrefab);
        
        players.Add(plr);
        plr.transform.position = spawn.transform.position + new Vector3(0, 0.5f, 0);print(plr.transform.position);
    }

    public void DespawnPlayer(Player plr)
    {
        players.Remove(plr);
        Destroy(plr.gameObject);
    }

    public IEnumerator LoadEnvironment()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(environment.name);
        
        while(loading.progress < 0.9f)
        {
            print("loading");
            yield return null;
        }
        SceneManager.SetActiveScene(scene);
    }

    public IEnumerator UnloadEnvironment()
    {
        AsyncOperation loading = SceneManager.UnloadSceneAsync(environment.name);

        while (loading.progress < 0.9f)
        {
            print("loading");
            yield return null;
        }
    }
}
