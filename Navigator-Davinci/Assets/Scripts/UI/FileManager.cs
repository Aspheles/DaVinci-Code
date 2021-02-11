using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;




public class FileManager : MonoBehaviour
{
    string _path;
    //string Path;
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

        //StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", "png", false, (string[] paths) => WriteResult(paths));
        // Set filters (optional)
        // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
        // if all the dialogs will be using the same filters
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

        // Set default filter that is selected when the dialog is shown (optional)
        // Returns true if the default filter is set successfully
        // In this case, set Images filter as the default filter
        FileBrowser.SetDefaultFilter(".jpg");

        // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
        // Note that when you use this function, .lnk and .tmp extensions will no longer be
        // excluded unless you explicitly add them as parameters to the function
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Users", "C:\\Users/Pictures", null);

        // Show a save file dialog 
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Show a select folder dialog 
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //						   () => { Debug.Log( "Canceled" ); },
        //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Coroutine example
        StartCoroutine(ShowLoadDialogCoroutine());

    }


    /// <summary>
    /// Gets the image in the path, and sets the gameobject texture to the image texture from the path.
    /// </summary>
    public void UpdateImage()
    {
        WWW www = new WWW(_path);
        img.texture = www.texture;

        Session.instance.question.image = img;

         
        largeImage.GetComponent<RawImage>().texture = www.texture;
        zoomBtn.SetActive(true);
    }

    /// <summary>
    /// Activates the Large Image game object.
    /// </summary>

    public void EnlargeImage()
    {
        largeImage.SetActive(true);
        cover.SetActive(true);
    }

    /// <summary>
    /// Deactivates the Large Image game object.
    /// </summary>
    public void CloseLargeImage()
    {
        largeImage.SetActive(false);
        cover.SetActive(false);
    }


    /// <summary>
    /// Opens the file explorer to choose your image.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                Debug.Log(FileBrowser.Result[i]);
                _path = FileBrowser.Result[i];
                Session.instance.image = FileBrowser.Result[i];
            }

            UpdateImage();
                

            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
        }
    }
}
