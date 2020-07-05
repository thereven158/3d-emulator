using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private bool _panMode = false;
    private bool _rotateMode = false;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform TargerObject;

    private Vector3 _previousPos;

    public void SetPanMode(bool active)
    {
        _panMode = active;
        _rotateMode = !active;
    }

    public void SetRotateMode(bool active)
    {
        _rotateMode = active;
        _panMode = !active;
    }

    public void DisableMode()
    {
        _rotateMode = false;
        _panMode = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _previousPos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = _previousPos - cam.ScreenToViewportPoint(Input.mousePosition);

            if (_panMode)
            {
                cam.transform.position += direction;
            }

            if (_rotateMode)
            {
                cam.transform.position = TargerObject.position;

                cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                cam.transform.Translate(new Vector3(0, 0, -10));

                _previousPos = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            
        }
    }
}
