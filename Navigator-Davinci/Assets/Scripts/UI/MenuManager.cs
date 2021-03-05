using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Menu[] menus;
    public static MenuManager instance;

    private void Start()
    {
        if (instance != null) return;
        instance = this;
    }

    /// <summary>
    /// Opens the specified menu, and closes the rest.
    /// </summary>
    /// <param name="menuName"></param>
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    /// <summary>
    /// Closes all the menus, that are not open. checks for the current menu and sets it active.
    /// </summary>
    /// <param name="menu"></param>
    public void OpenMenu(Menu menu)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }


    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
