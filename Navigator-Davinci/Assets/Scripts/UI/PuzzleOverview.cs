using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class PuzzleOverview : MonoBehaviour
{ 
    [SerializeField] private Transform container;
    [SerializeField] private GameObject puzzleObject;
    [SerializeField] public GameObject cover;
    public static PuzzleOverview instance;
    public List<PuzzleData> puzzles;
    public Puzzle selectedPuzzle;
    public TMP_Dropdown difficultyDropdown;
    public TMP_InputField searchbar;
    public TMP_Dropdown filterDropdown;

    public GameObject difficultyPanel;
    public GameObject namePanel;

    [Header("Detail page")]
    [SerializeField] TMP_Text puzzleName;
    [SerializeField] TMP_Text puzzleDescription;
    [SerializeField] TMP_Text puzzleDifficulty;
    [SerializeField] TMP_Text puzzleCreator;

    [Header("Edit page")]
    public TMP_Text puzzleEditName;
    public TMP_Text puzzleEditDescription;
    public TMP_Dropdown puzzleEditDifficulty;
    public TMP_Text message;


    private void Awake()
    {
        instance = this;
        cover.SetActive(false);
        LoadPuzzles();
    }

    private void Update()
    {
        if(Session.instance.message != null)
        {
            message.text = Session.instance.ErrorHandling();
        }

        if (filterDropdown.options[filterDropdown.value].text == "Sort by Name")
        {
            namePanel.SetActive(true);
            difficultyPanel.SetActive(false);
        }else{
            namePanel.SetActive(false);
            difficultyPanel.SetActive(true);

            //Puzzles need to be filtered
            LoadPuzzles(true);
        }


    }

    /// <summary>
    /// Loads all the puzzles.
    /// </summary>
    public void LoadPuzzles(bool hasDifficulty=false)
    {
        //puzzles = new List<PuzzleData>();

        for (int o = 0; o < container.childCount; o++)
        {
            Destroy(container.GetChild(o).gameObject);
        }

        if (puzzles.Count > 0 && puzzles != null)
        {

            if (hasDifficulty)
            {
                foreach (PuzzleData puzzle in puzzles)
                {
                    if (puzzle.difficulty == difficultyDropdown.options[difficultyDropdown.value].text)
                    {
                        GameObject puzzleCopyObject = Instantiate(puzzleObject, container.position, Quaternion.identity);
                        puzzleCopyObject.transform.SetParent(container);
                        puzzleCopyObject.GetComponent<Puzzle>().id = puzzle.id;
                        puzzleCopyObject.GetComponent<Puzzle>().name.text = puzzle.name;
                        puzzleCopyObject.GetComponent<Puzzle>().description = puzzle.description;
                        puzzleCopyObject.GetComponent<Puzzle>().difficulty.text = puzzle.difficulty;
                        puzzleCopyObject.GetComponent<Puzzle>().creator = puzzle.creator;
                    }

                }
            }
            else
            {
                foreach (PuzzleData puzzle in puzzles)
                {
                  
                    GameObject puzzleCopyObject = Instantiate(puzzleObject, container.position, Quaternion.identity);
                    puzzleCopyObject.transform.SetParent(container);
                    puzzleCopyObject.GetComponent<Puzzle>().id = puzzle.id;
                    puzzleCopyObject.GetComponent<Puzzle>().name.text = puzzle.name;
                    puzzleCopyObject.GetComponent<Puzzle>().description = puzzle.description;
                    puzzleCopyObject.GetComponent<Puzzle>().difficulty.text = puzzle.difficulty;
                    puzzleCopyObject.GetComponent<Puzzle>().creator = puzzle.creator;
                    

                }
            }
           

            
        }
    }

    public void OrderPuzzles()
    {
        puzzles = puzzles.OrderBy(x => x.name).ToList();

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

            puzzleName.text = puzzleinfo.name.text;
            puzzleDifficulty.text = "Difficulty: " + puzzleinfo.difficulty.text;
            puzzleCreator.text = "Created by: " + puzzleinfo.creator;
            puzzleDescription.text = puzzleinfo.description;
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

            puzzleEditName.text = selectedPuzzle.name.text;
            puzzleEditDescription.text = selectedPuzzle.description;


        }
        else if (editPage.activeSelf == true)
        {
            //editPage.SetActive(false);
            //print(puzzleinfo.id + puzzleinfo.name.text + puzzleinfo.description);
            //StartCoroutine(EditPuzzle(puzzleinfo.id, puzzleEditName.text, puzzleEditDescription.text, puzzleEditDifficulty.options[puzzleEditDifficulty.value].text));

            new Puzzles().Edit();
        }

    }

    

   
}
