using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;

    /// <summary>
    /// Opens the menu.
    /// </summary>

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Closes the menu.
    /// </summary>
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
    
}
