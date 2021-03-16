using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalSpawnPoints : MonoBehaviour
{
    public List<GameObject> spawnpoints;
    public GameObject terminalObject;

    private void Start()
    {
        LoadTerminals();
    }
    public void LoadTerminals()
    {
        for(int i = 0; i < spawnpoints.Count; i++)
        {
            //Terminal terminalCopy = terminalObject.GetComponent<Terminal>();
            GameObject terminalCopy = Instantiate(terminalObject, spawnpoints[i].transform.position, Quaternion.identity);
            terminalCopy.GetComponent<Terminal>().number = "0";

            terminalCopy.transform.SetParent(spawnpoints[i].transform);
            terminalCopy.transform.rotation = spawnpoints[i].transform.rotation;
        }
    }
}