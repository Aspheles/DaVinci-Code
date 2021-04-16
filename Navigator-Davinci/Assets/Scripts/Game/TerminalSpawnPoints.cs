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
        
        if(GameObject.FindGameObjectsWithTag("Terminal").Length > 0)
        {
            for(int i = 0; i < spawnpoints.Count; i++)
            {
                Destroy(spawnpoints[i].GetComponentInChildren<Terminal>().gameObject);
            }
        }

        for(int i = 0; i < spawnpoints.Count; i++)
        {
            //Terminal terminalCopy = terminalObject.GetComponent<Terminal>();
            GameObject terminalCopy = Instantiate(terminalObject, spawnpoints[i].transform.position, Quaternion.identity);

            terminalCopy.transform.SetParent(spawnpoints[i].transform);
            terminalCopy.transform.rotation = spawnpoints[i].transform.rotation;

            terminalCopy.GetComponent<Terminal>().progress = Terminal.ScreenProgress.READY;

         
            //Check puzzles if they don't match with others
            terminalCopy.GetComponent<Terminal>().LoadPuzzle(difficultyList[i]);

            //set questions to the correct terminal
            //RunManager.instance.SetPuzzleQuestions(terminalCopy.GetComponent<Terminal>());

            // The Terminal copy gets added to the List of terminals in the room script.

            Room.instance.terminals.Add(terminalCopy.GetComponent<Terminal>());
        }
    }

    public void GetDifficulty()
    {
        if (Room.instance.roomNumber <= 1 || UserInfo.instance.selectedDifficulty == "Hard")
        {
            AssignDifficulty(Room.instance.difficulty, 6);
        }
        else
        {

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

    public void AssignDifficulty(string difficulty, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            difficultyList.Add(difficulty);
        }
    }

}