using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerPuzzlePad : MonoBehaviour
{
    public string terminalName;
    public TMP_Text confirmText;
    public Material mat;
    public GameObject puzzlePanel;

    private void Update()
    {
    }
    private void Start()
    {
        confirmText = GameObject.Find("StartTerminal").GetComponent<TextMeshProUGUI>();
        puzzlePanel = GameObject.Find("TerminalPuzzle");
        puzzlePanel.SetActive(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            confirmText.text = "Press 'E' to start the terminal";
            if(Input.GetKey(KeyCode.E))
            {
                mat.color = Color.white;
                puzzlePanel.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        confirmText.text = "";
    }
}
