using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    private GameObject _getTarget;
    private bool _isMouseDragging = false;
    private Vector3 _offsetValue;
    private Vector3 _positionOfScreen;

    private Transform[] _models;
    private Vector3[] defaultPos;
    private Quaternion[] defaultRot;
    
    private float _zCoord;

    [SerializeField]
    private float _rotSpeed = 200f;
    
    private bool _dragMode = false;
    private bool _rotateMode = false;

    void Start()
    {
        StoreDefaultPosition();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetTargetObject();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _isMouseDragging = false;
        }
        
        if (_isMouseDragging)
        {
            if(_dragMode) DragObject();

            if (_rotateMode) RotateObject();
        }
    }

    private void StoreDefaultPosition()
    {
        GameObject[] tempModels = GameObject.FindGameObjectsWithTag("Model");

        defaultPos = new Vector3[tempModels.Length];
        defaultRot = new Quaternion[tempModels.Length];
        _models = new Transform[tempModels.Length];

        for(int i = 0; i < tempModels.Length; i++)
        {
            _models[i] = tempModels[i].GetComponent<Transform>();

            defaultPos[i] = _models[i].position;
            defaultRot[i] = _models[i].rotation;
        }
    }

    public void ResetPositionModels()
    {
        for(int i = 0; i < _models.Length; i++)
        {
            _models[i].position = defaultPos[i];
            _models[i].rotation = defaultRot[i];
        }
    }

    private void GetTargetObject()
    {
        RaycastHit hitInfo;
        _getTarget = ClickedObject(out hitInfo);
        if (_getTarget != null)
        {
            _isMouseDragging = true;
            //Converting world position to screen position.
            _positionOfScreen = Camera.main.WorldToScreenPoint(_getTarget.transform.position);
            _offsetValue = _getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _positionOfScreen.z));
        }
    }

    private void DragObject()
    {
        //tracking mouse position.
        Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _positionOfScreen.z);

        //converting screen position to world position with offset changes.
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + _offsetValue;

        //It will update target gameobject's current postion.
        _getTarget.transform.position = currentPosition;
    }

    private void RotateObject()
    {
        float rotX = Input.GetAxis("Mouse X") * _rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * _rotSpeed * Mathf.Deg2Rad;

        _getTarget.transform.Rotate(new Vector3(0, 1, 0), -rotX);
        _getTarget.transform.Rotate(new Vector3(1, 0, 0), rotY);
    }

    GameObject ClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    public void SetDragMode(bool active)
    {
        _dragMode = active;
        _rotateMode = !active;
    }

    public void SetRotateMode(bool active)
    {
        _rotateMode = active;
        _dragMode = !active;
    }

    public void DisableMode()
    {
        _rotateMode = false;
        _dragMode = false;
    }

    void OnMouseDown()
    {
        _zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Debug.Log(_zCoord);
    }
    
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = _zCoord;

        return Camera.main.ScreenToViewportPoint(mousePos);
    }
    
   
}
