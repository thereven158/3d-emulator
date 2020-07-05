using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InstantiateButton : MonoBehaviour
{
    private string _urlDirectory;

    public int LoadVideos(string directory)
    {
        int i = 0;

        DirectoryInfo root = new DirectoryInfo(directory);

        FileInfo[] _fileInfo = root.GetFiles();

        foreach (FileInfo fileInfo in _fileInfo)
        {
            if (fileInfo.Extension.Contains("mp4"))
                i++;
        }

        return i;
    }

    // Start is called before the first frame update
    void Start()
    {
        _urlDirectory = Application.streamingAssetsPath + "/" + "Videos/";

        Debug.Log(LoadVideos(_urlDirectory));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
