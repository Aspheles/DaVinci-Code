using NUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject nextPortal;

    [SerializeField] Environment leave;
    public string enter;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Player plr = collision.gameObject.GetComponent<Player>();
            leave.DespawnPlayer(plr);
            AsyncOperation loading = SceneManager.LoadSceneAsync(enter);
            Environment enterEnv = GameObject.Find(enter).GetComponent<Environment>();
            enterEnv.SpawnPlayer(enterEnv.spawnPoint);
            StartCoroutine(LoadNextScene(loading, enterEnv));
        }
    }

    private IEnumerator LoadNextScene(AsyncOperation loading, Environment env)
    {
        while (loading.progress < 0.9f)
        {
            yield return null;
        }
        env.SpawnPlayer(env.spawnPoint);
    }
}
