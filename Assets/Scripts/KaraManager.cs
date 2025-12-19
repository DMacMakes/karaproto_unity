using SimpleFileBrowser;
using UnityEngine;
using static SimpleFileBrowser.FileBrowser;
using eToile;
//static extern bool ShowSaveDialog(OnSuccess onSuccess, OnCancel onCancel, PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Save", string saveButtonText = "Save");
//static extern bool ShowLoadDialog(OnSuccess onSuccess, OnCancel onCancel, PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Load", string loadButtonText = "Select");

//public delegate void OnSuccess(string[] paths);
//public delegate void OnCancel();

public class KaraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set filters (optional)
        // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
        // if all the dialogs will be using the same filters
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Audio Files", ".wav", ".ogg", ".mp3"));

        // Set default filter that is selected when the dialog is shown (optional)
        // Returns true if the default filter is set successfully
        // In this case, set Images filter as the default filter
        FileBrowser.SetDefaultFilter(".wav");

    }

    //public delegate void OnSuccess(string[] paths);
    //public delegate void OnCancel();

    public void onLoadWavClicked()
    {
        Debug.Log($"You told kara manager that loadwav was clicked.");
        ShowLoadDialog(fileChosen, cancelled, FileBrowser.PickMode.Files, false, null, null, "Load Wav File", "Load");
    }

    void fileChosen(string[] paths)
    {
        Debug.Log($"Load {paths[0]}");
        string filePath = paths[0];

        // Read the bytes of the first file via FileBrowserHelpers
        // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
        byte[] wavBytes = FileBrowserHelpers.ReadBytesFromFile(filePath);
        Debug.Log($"Loaded {wavBytes.Length} bytes.");
        AudioClip wavClip = OpenWavParser.ByteArrayToAudioClip(wavBytes, "War Figs");
        Debug.Log("wavClip now (maybe) holds a sweet wav AudioClip");
    }

    void cancelled()
    {
        Debug.Log("File selection cancelled.");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
