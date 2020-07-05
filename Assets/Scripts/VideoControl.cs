using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private VideoPlayer _videoPlayer;

    [SerializeField]
    private Slider _slider;

    private bool _sliding;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
        _sliding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float frame = _slider.value * _videoPlayer.frameCount;
        _videoPlayer.frame = (long)frame;
        _sliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _sliding = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float frame = _slider.value * _videoPlayer.frameCount;
            _videoPlayer.frame = (long)frame;
            _sliding = false;
        }

        if (!_sliding) _slider.value = _videoPlayer.frame / (float)_videoPlayer.frameCount;
    }
}
