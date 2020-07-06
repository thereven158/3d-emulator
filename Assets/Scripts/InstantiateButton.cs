using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InstantiateButton : MonoBehaviour
{
    private string _urlDirectory;
	private string _nameVideo = "cat";

	[SerializeField]
	private GameObject _btnPrefab;

	[SerializeField]
	private GameObject _videoBtnsObj;

	[SerializeField]
	private VideoLoader _videoLoader;
	
	[SerializeField]
	private UIControl _uiControl;

    public int CountVideos(string directory)
    {
        int i = 0;

        DirectoryInfo root = new DirectoryInfo(directory);

        FileInfo[] _fileInfo = root.GetFiles();

        foreach (FileInfo fileInfo in _fileInfo)
        {
            if (fileInfo.Extension.Contains("mp4")){
				i++;
				int temp = i;
				CreateButton(temp);
			}
                
        }

        return i;
    }

    // Start is called before the first frame update
    void Start()
    {
        _urlDirectory = Application.streamingAssetsPath + "/" + "Videos/";

        CountVideos(_urlDirectory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void CreateButton(int number){
		GameObject newButton = Instantiate(_btnPrefab) as GameObject;
		newButton.transform.SetParent(_videoBtnsObj.transform, false);

		Button btnComponent = newButton.GetComponent<Button>();

		string nameFile = _nameVideo + number;

		btnComponent.onClick.AddListener(() => {
			_videoLoader.PlayStreamingVideo(nameFile);
			_uiControl.EnableVideoControlUI();
			_uiControl.DisableMainUI();
		});
	}

}
