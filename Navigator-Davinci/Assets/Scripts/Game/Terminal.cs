using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public string number;
    public Material mat;
    public GameObject terminal;
    public GameObject screen;
    public Material terminalScreenmat;
    public ScreenProgress progress;
    public Material terminalmat;

    private void Start()
    {
        terminalScreenmat = screen.GetComponent<MeshRenderer>().material;
    }

    public enum ScreenProgress
    {
        READY,
        BLOCKED,
        PROGRESS,
        FINISHED,
        FAILED
    }

    void CheckTerminalProgress(ScreenProgress progress)
    {
        switch (progress)
        {
            case ScreenProgress.READY:
                terminalScreenmat.color = new Color32(77, 121, 255, 255);
                break;
            case ScreenProgress.BLOCKED:
                terminalScreenmat.color = Color.black;
                break;
            case ScreenProgress.PROGRESS:
                terminalScreenmat.color = Color.yellow;
                break;
            case ScreenProgress.FINISHED:
                terminalScreenmat.color = Color.green;
                break;
            case ScreenProgress.FAILED:
                terminalScreenmat.color = Color.red;
                break;
        }
    }


    private void Update()
    {
        mat.color = Color.red;
        CheckTerminalProgress(progress);

    }
    public void GetData(JSONNode data)
    {
        number = data[0].AsObject["number"];
    }

}
