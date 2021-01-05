using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileManager : MonoBehaviour
{

    string Path;
    public RawImage img;
    public GameObject largeImage;
    public GameObject zoomBtn;
    public GameObject cover;

    void Start()
    {
        
        img.texture = Resources.Load<Texture>("images/defaultimg");
        zoomBtn.SetActive(false);
        
    }
    public void OpenExplorer()
    {
        Path = EditorUtility.OpenFilePanel("overwrite with png", "", "png");
        GetImage();
    }

    public void GetImage()
    {
        if(Path != null && Path.Length > 0)
        {
            UpdateImage();
        }

    }

    public void UpdateImage()
    {
        WWW www = new WWW("file:///" + Path);
        img.texture = www.texture;

        largeImage.GetComponent<RawImage>().texture = www.texture;
        zoomBtn.SetActive(true);
    }

    public void EnlargeImage()
    {
        largeImage.SetActive(true);
        cover.SetActive(true);
    }

    public void CloseLargeImage()
    {
        largeImage.SetActive(false);
        cover.SetActive(false);
    }
}
