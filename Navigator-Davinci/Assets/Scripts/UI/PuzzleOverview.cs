using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;


public class PuzzleOverview : MonoBehaviour
{ 
    [SerializeField] private Transform container;
    [SerializeField] private GameObject puzzleObject;
    [SerializeField] private GameObject cover;
    public static PuzzleOverview instance;
    public List<PuzzleData> puzzles;
    private void Awake()
    {
        instance = this;
        cover.SetActive(false);
    }

    /// <summary>
    /// Loads all the puzzles.
    /// </summary>
    public void LoadPuzzles()
    {
        for (int o = 0; o < container.childCount; o++)
        {
            Destroy(container.GetChild(o).gameObject);
        }
        if (puzzles.Count > 0 && puzzles != null)
        {
            foreach(PuzzleData puzzle in puzzles)
            {
                GameObject puzzleCopyObject = Instantiate(puzzleObject, container.position, Quaternion.identity);
                puzzleCopyObject.transform.SetParent(container);
                puzzleCopyObject.GetComponent<Puzzle>().id = puzzle.id;
                puzzleCopyObject.GetComponent<Puzzle>().name.text = puzzle.name;
                puzzleCopyObject.GetComponent<Puzzle>().description = puzzle.description;
                puzzleCopyObject.GetComponent<Puzzle>().difficulty = puzzle.difficulty;
                puzzleCopyObject.GetComponent<Puzzle>().creator = puzzle.creator;
            }
        }
    }
    public void Open()
    {
        
        Launcher.instance.OpenPuzzleQuestionsOverviewMenu();
    }

    public void New()
    {
        Launcher.instance.OpenPuzzleCreatorMenu();
    }

    /// <summary>
    /// Deletes chosen puzzle.
    /// </summary>
    /// <param name="puzzle"></param>
    public IEnumerator Delete(Puzzle puzzle)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", puzzle.id.ToString())
        };

        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/deletepuzzle.php", form);
        
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        else
        {
            Destroy(puzzle.puzzleObject);
            print(www.downloadHandler.text);
        }
    }

    /// <summary>
    /// Opens the info modal for the chosen puzzle.
    /// </summary>
    /// <param name="puzzleinfo"></param>
    public void OpenInfo(Puzzle puzzleinfo)
    {
        GameObject editPage = cover.transform.Find("edit").gameObject;
        GameObject detailPage = cover.transform.Find("details").gameObject;
        print(editPage);
        if (cover.activeSelf != true)
        {
            cover.SetActive(true);
            detailPage.SetActive(true);
            editPage.SetActive(false);

            detailPage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = puzzleinfo.name.text;
            detailPage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Difficulty: " + puzzleinfo.difficulty;
            detailPage.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Created by: " + puzzleinfo.creator;
            detailPage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = puzzleinfo.description;
        }
        else if(cover.activeSelf != false)
        {
            cover.SetActive(false);
        }
        
    }

    public void OpenEditInfo(Puzzle puzzleinfo)
    {
        GameObject editPage = cover.transform.Find("edit").gameObject;
        GameObject detailPage = cover.transform.Find("details").gameObject;

        if (editPage.activeSelf == false)
        {
            detailPage.SetActive(false);
            editPage.SetActive(true);
            
            editPage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Difficulty: " + puzzleinfo.difficulty;
           
        }
        else if (editPage.activeSelf == true)
        {
            editPage.SetActive(false);print(puzzleinfo.id + puzzleinfo.name.text + puzzleinfo.description);
            StartCoroutine(EditPuzzle(puzzleinfo.id, puzzleinfo.name.text, puzzleinfo.description));

        }

    }

    public IEnumerator EditPuzzle(int id, string title, string description)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", id.ToString()),
            new MultipartFormDataSection("title", title),
            new MultipartFormDataSection("description", description)
        };
        
        UnityWebRequest www = UnityWebRequest.Post("http://davinci-code.nl/editpuzzle.php", form);
        
        yield return www.SendWebRequest();
        Launcher.instance.OpenAdminPuzzleOverviewMenu();
    }

    /// <summary>
    /// Fetches all the puzzles from the database.
    /// </summary>
    /// <returns></returns>

    public IEnumerator FetchPuzzles()
    {
        puzzles = new List<PuzzleData>();

        UnityWebRequest www = UnityWebRequest.Get("http://davinci-code.nl/fetchpuzzles.php");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        else
        {
            byte[] puzzleInfo = www.downloadHandler.data;
            string result = System.Text.Encoding.Default.GetString(puzzleInfo);

            JSONArray jsonData = JSON.Parse(result) as JSONArray;

            for(int i = 0; i < jsonData.Count; i++)
            {
                PuzzleData newPuzzle = new PuzzleData(int.Parse(jsonData[i].AsObject["id"]), jsonData[i].AsObject["name"], jsonData[i].AsObject["difficulty"], jsonData[i].AsObject["description"], jsonData[i].AsObject["creator"]);
                puzzles.Add(newPuzzle);
            }
            
            if(puzzles.Count > 0)
            {
                LoadPuzzles();
            }
        }
    }
}
