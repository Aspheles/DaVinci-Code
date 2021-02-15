using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Wall : MonoBehaviour
{

    [SerializeField] string sceneName;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
