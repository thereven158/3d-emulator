using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    private bool _panMode = false;
    private bool _rotateMode = false;
	private bool _zoomMode = false;

	private int _countClick;
	private float _timer;

	private Vector3 _defaultCamPos;
	private Quaternion _defaultCamRot;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform TargerObject;

	[SerializeField]
	private GameObject _sliderObj;

	private Slider _sliderZoom;

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

	void Awake(){
		_sliderZoom = _sliderObj.GetComponent<Slider>();
		_sliderZoom.value = _sliderZoom.maxValue;
	}

    // Start is called before the first frame update
    void Start()
    {
        _defaultCamPos = cam.transform.position;
		_defaultCamRot = cam.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
		_timer += Time.deltaTime;
		
		if(_timer > 0.5){
			_countClick = 0;
			_timer = 0;
		}

        if(Input.GetMouseButtonDown(0))
        {
			_countClick++;
			
			if(_countClick == 2){
				_zoomMode = true;
			}
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

		if(_zoomMode){
			_sliderObj.SetActive(true);
			if(Input.GetMouseButton(0)){
				cam.fieldOfView = _sliderZoom.value;
			}
			
		}
    }

	public void DisableZoom(){
		_sliderObj.SetActive(false);
		_zoomMode = false;
	}
	
	public void ResetCamera(){
		cam.transform.position = _defaultCamPos;
		cam.transform.rotation = _defaultCamRot;
	}
}
