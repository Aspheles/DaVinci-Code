using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalSpawnPoints : MonoBehaviour
{
    public List<GameObject> spawnpoints;
    public GameObject terminalObject;
    public List<string> difficultyList;
    public static TerminalSpawnPoints instance;
    public bool terminalsLoaded = false;
    public List<PuzzleData> selectedPuzzles;

    private void Start()
    {
        instance = this;
        //GetDifficulty();
    }

    public void LoadTerminals()
    {

        RunManager.instance.randomizedPuzzles = new List<PuzzleData>();

        if (GameObject.FindGameObjectsWithTag("Terminal").Length > 0)
        {
            for(int i = 0; i < spawnpoints.Count; i++)
            {
                if(spawnpoints[i].GetComponentInChildren<Terminal>() != null)
                Destroy(spawnpoints[i].GetComponentInChildren<Terminal>().gameObject);
            }
        }

        for (int i = 0; i < spawnpoints.Count; i++)
        {
            //Terminal terminalCopy = terminalObject.GetComponent<Terminal>();
            GameObject terminalCopy = Instantiate(terminalObject, spawnpoints[i].transform.position, Quaternion.identity);

            terminalCopy.transform.SetParent(spawnpoints[i].transform);
            terminalCopy.transform.rotation = spawnpoints[i].transform.rotation;

            terminalCopy.GetComponent<Terminal>().progress = Terminal.ScreenProgress.READY;
            terminalCopy.GetComponent<Terminal>().terminalNumber = i;
            terminalCopy.GetComponent<Terminal>().difficulty = difficultyList[i];

            LoadPuzzles(difficultyList[i]);


            //Load the puzzle in the terminal
            if (Room.instance.roomNumber == 1)
            {
                //needs to be changed
                //terminalCopy.GetComponent<Terminal>().puzzle = LoadPuzzles(terminalCopy.GetComponent<Terminal>().difficulty, false);

                //LoadPuzzles(terminalCopy.GetComponent<Terminal>().difficulty, false);
                terminalCopy.GetComponent<Terminal>().LoadPuzzle(false);
            }
            else
            {
                //Now we have to check which puzzles has been done already from the database so it can be loaded in
                //Fetch from api request

                //Debug.Log("Completed puzzles count: " + RunManager.instance.completedPuzzles.Count);

                if(RunManager.instance.completedPuzzles.Count > 0)
                {
                    //LoadPuzzles(terminalCopy.GetComponent<Terminal>().difficulty, true);
                    terminalCopy.GetComponent<Terminal>().LoadPuzzle(true);
                }
                
            }
            

            //set questions to the correct terminal
            //RunManager.instance.SetPuzzleQuestions(terminalCopy.GetComponent<Terminal>());

            // The Terminal copy gets added to the List of terminals in the room script.

            Room.instance.terminals.Add(terminalCopy.GetComponent<Terminal>());
        }
    }

    public void GetDifficulty()
    {
        if (Room.instance.roomNumber <= 1 || UserInfo.instance.selectedDifficulty == "Hard" || Room.instance.difficulty == "Hard")
        {
            AssignDifficulty(Room.instance.difficulty, 6);
        }
        else
        {
            difficultyList = new List<string>();

            switch (Room.instance.roomNumber)
            {
                case 2:
                    if (Room.instance.difficulty == "Easy")
                    {
                        //5x easy
                        //1x medium
                        for (int i = 0; i < 5; i++)
                        {
                            difficultyList.Add("Easy");
                        }
                        difficultyList.Add("Medium");
                    }
                    else
                    {
                        //5x medium
                        //1x hard
                        for (int i = 0; i < 5; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        difficultyList.Add("Hard");
                    }
                    break;

                case 3:
                    if (Room.instance.difficulty == "Easy")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            difficultyList.Add("Easy");
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        //4x easy
                        //2x medium
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            difficultyList.Add("Hard");
                        }
                        //4x medium
                        //2x hard
                    }
                    break;

                case 4:
                    if (Room.instance.difficulty == "Easy")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            difficultyList.Add("Easy");
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        //3x easy
                        //3x medium
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            difficultyList.Add("Hard");
                        }
                        //3x medium
                        //3x hard
                    }
                    break;

                case 5:
                    if (Room.instance.difficulty == "Easy")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            difficultyList.Add("Easy");
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        //2x easy
                        //4x medium
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            difficultyList.Add("Hard");
                        }
                        //2x medium
                        //4x hard
                    }
                    break;
                case 6:
                    if (Room.instance.difficulty == "Easy")
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            difficultyList.Add("Easy");
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        //1x easy
                        //5x medium
                    }
                    else
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            difficultyList.Add("Medium");
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            difficultyList.Add("Hard");
                        }
                        //1x medium
                        //5x hard
                    }
                    break;
            }
        }
        LoadTerminals();
    }

    public void LoadPuzzles(string difficulty)
    {
        Debug.Log("Loading puzzle");
       
        foreach (PuzzleData puzzle in RunManager.instance.puzzles)
        {
            if (!RunManager.instance.randomizedPuzzles.Contains(puzzle))
            {
                if (difficulty == puzzle.difficulty)
                {
                    RunManager.instance.randomizedPuzzles.Add(puzzle);
                    return;
                }
            }

        }
        
        

    }

    public void AssignDifficulty(string difficulty, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            difficultyList.Add(difficulty);
        }
    }

    public void ResetTerminals()
    {
        int terminalCount = GameObject.FindGameObjectsWithTag("Terminal").Length;

        if(terminalCount > 0)
        {
            foreach(GameObject terminal in GameObject.FindGameObjectsWithTag("Terminal"))
            {
                Destroy(terminal);
            }

            GetDifficulty();
        }
    }

}