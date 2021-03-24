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

            terminalCopy.transform.SetParent(spawnpoints[i].transform);
            terminalCopy.transform.rotation = spawnpoints[i].transform.rotation;

            if(i == 0)
            {
                terminalCopy.GetComponent<Terminal>().progress = Terminal.ScreenProgress.READY;
            }
            else
            {
                terminalCopy.GetComponent<Terminal>().progress = Terminal.ScreenProgress.BLOCKED;
            }


            //Check puzzles if they don't match with others
            terminalCopy.GetComponent<Terminal>().LoadPuzzle();

            //set questions to the correct terminal
            //RunManager.instance.SetPuzzleQuestions(terminalCopy.GetComponent<Terminal>());

            // The Terminal copy gets added to the List of terminals in the room script.

            Room.instance.terminals.Add(terminalCopy.GetComponent<Terminal>());
        }
    }
}