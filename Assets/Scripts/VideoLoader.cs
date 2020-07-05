using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    private Dictionary<string, string> _videoName;

    private string _videosFile = "Videos\\";
    private string _extensionFile = ".mp4";

    public VideoPlayer _videoPlayer;

    public static string GetFileLocation(string relativePath)
    {
        return "file://" + Path.Combine(Application.streamingAssetsPath, relativePath);
    }

    public void PlayStreamingVideo(string videoFile)
    {
        _videoPlayer.source = VideoSource.Url;
        _videoPlayer.url = Application.streamingAssetsPath + "/" + "Videos/" + videoFile + _extensionFile;
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        var audioSource = _videoPlayer.GetComponent<AudioSource>();
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.controlledAudioTrackCount = 1;
        _videoPlayer.EnableAudioTrack(0, true);
        _videoPlayer.SetTargetAudioSource(0, audioSource);

        _videoPlayer.Prepare();
        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }

        _videoPlayer.Play();
        
    }
    
}
