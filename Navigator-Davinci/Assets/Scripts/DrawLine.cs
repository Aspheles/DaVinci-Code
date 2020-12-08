using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    Material mat;
    Vector3 startVertex;
    Vector3 mousePos;

    private void Start()
    {
        startVertex = Vector3.zero;
    }

    private void Update()
    {
        mousePos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
        }
    }


    private void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please assign a material");
            return;
        }

        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();


        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));

        GL.End();
        GL.PopMatrix();
        
    }


   
}
