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
    [SerializeField] Environment enter;

    [SerializeField] public GameObject confirm;
    Player player;

    bool status;

    private void Start()
    {
        confirm.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            player = collision.gameObject.GetComponent<Player>();
            confirm.SetActive(true);

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void CancelNow()
    {
        confirm.SetActive(false);
        player.transform.position = spawnPoint.transform.position + new Vector3(0, 1.5f, 0);
    }

    public void LoadNow()
    {
        confirm.SetActive(false);

        Player plr = player.gameObject.GetComponent<Player>();
        //leave.DespawnPlayer(plr);
        AsyncOperation loading = SceneManager.LoadSceneAsync(enter.name);
        //enter.SpawnPlayer(enter.spawnPoint);
        StartCoroutine(LoadNextScene(loading, enter));

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
