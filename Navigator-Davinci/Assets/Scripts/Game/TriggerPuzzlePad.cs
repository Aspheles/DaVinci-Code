using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerPuzzlePad : MonoBehaviour
{
    public string terminalName;
    public TMP_Text confirmText;
    public Material mat;

    private void Update()
    {
        mat.color = Color.blue;
    }
    private void Start()
    {
        confirmText = GameObject.Find("StartTerminal").GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            confirmText.text = "Press 'E' to start the terminal";
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        confirmText.text = "";
    }
}
