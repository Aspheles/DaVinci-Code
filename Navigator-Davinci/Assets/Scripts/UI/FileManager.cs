using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileManager : MonoBehaviour
{

    string Path;
    public RawImage img;

    void Start()
    {
        
       img.texture = Resources.Load<Texture>("images/defaultimg");
        
    }
    public void OpenExplorer()
    {
        Path = EditorUtility.OpenFilePanel("overwrite with png", "", "png");
        GetImage();
    }

    public void GetImage()
    {
        if(Path != null)
        {
            UpdateImage();
        }
    }

    public void UpdateImage()
    {
        WWW www = new WWW("file:///" + Path);
        img.texture = www.texture;
    }
}
