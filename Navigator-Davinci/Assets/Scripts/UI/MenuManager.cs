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
