using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _uiControlGameObject;

    [SerializeField]
    private GameObject _uiVideoControl;

    public void DisableMainUI()
    {
        for (int i = 0; i < _uiControlGameObject.Length; i++)
        {
            var temp = i;
            _uiControlGameObject[i].SetActive(false);
        }
    }

    public void EnableMainUI()
    {
        for (int i = 0; i < _uiControlGameObject.Length; i++)
        {
            var temp = i;
            _uiControlGameObject[i].SetActive(true);
        }
    }

    public void EnableVideoControlUI()
    {
        _uiVideoControl.SetActive(true);
    }

    public void DisableVideoControlUI()
    {
        _uiVideoControl.SetActive(false);
    }

    private void Start()
    {
        DisableVideoControlUI();
    }
}
