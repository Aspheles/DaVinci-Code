using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject ScoreView;
    public void ShowResult(int questions, int correct)
    {

        scoreText.text = correct.ToString() + "/" + questions.ToString();
    }

    public void CloseResult()
    {
        ScoreView.SetActive(false);
        Cursor.visible = false;
        GameCamera.instance.canMove = true;
        Player.instance.canwalk = true;
        Player.instance.player.transform.position = RunManager.instance.startingPosition.position;
    }
}
