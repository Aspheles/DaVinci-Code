using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseHover : MonoBehaviour
{
    void OnMouseOver()
    {
        Debug.Log("I'm on the " + gameObject.name);
    }
    private void OnMouseEnter()
    {
        
    }
    private void OnMouseExit()
    {
        
    }
}
