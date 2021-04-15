using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    public TMP_Dropdown Difficulty;

    public void ConfirmDifficulty()
    {
        UserInfo.instance.selectedDifficulty = Difficulty.options[Difficulty.value].text;
        Launcher.instance.LoadRoom();
    }

   
}
