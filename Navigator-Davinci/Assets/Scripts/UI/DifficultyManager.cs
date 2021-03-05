using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] GameObject alertPanel;

    private void Start()
    {
        alertPanel.SetActive(false);
    }
    public void OnNextClicked()
    {
        alertPanel.SetActive(true);
    }

    public void OnOkClicked()
    {
        alertPanel.SetActive(false);
    }
}
