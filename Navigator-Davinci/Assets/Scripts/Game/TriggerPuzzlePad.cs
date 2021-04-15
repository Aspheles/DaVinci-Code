using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerPuzzlePad : MonoBehaviour
{
    public Terminal terminal;
    public TMP_Text confirmText;
    public Material mat;
    public GameObject puzzlePanel;

    private void Update()
    {
    }
    private void Start()
    {
        confirmText = GameObject.Find("StartTerminal").GetComponent<TextMeshProUGUI>();
        //terminal.GetQuestions();

    }

    private void OnCollisionStay(Collision collision)
    {
        if(terminal.progress == Terminal.ScreenProgress.READY || terminal.progress == Terminal.ScreenProgress.PROGRESS)
        {
            if (collision.gameObject.tag == "Player")
            {
                RunManager.instance.terminal = terminal;
                confirmText.text = "Press 'E' to start the terminal";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //mat.color = Color.white;
                    RunManager.instance.OpenPuzzle();
                    terminal.progress = Terminal.ScreenProgress.PROGRESS;
                    terminal.GetQuestions();
                    RunManager.instance.loadingScreen.SetActive(true);
        
                }

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        confirmText.text = "";
    }
}
