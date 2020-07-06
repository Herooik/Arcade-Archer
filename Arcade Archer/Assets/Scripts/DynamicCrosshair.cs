using UnityEngine;

public class DynamicCrosshair : MonoBehaviour
{
    [SerializeField] GameObject _mainCamera;
    [SerializeField] GameObject _crosshairUp;
    [SerializeField] GameObject _crosshairDown;
    [SerializeField] GameObject _crosshairLeft;
    [SerializeField] GameObject _crosshairRight;
    [SerializeField] float _spread = 0;

    float _tempSpread;
    float _initialPosition;
    float _initialCameraPosition;

    void Start()
    {
        _initialPosition = _crosshairUp.GetComponent<Transform>().localPosition.y;
        _initialCameraPosition = _mainCamera.GetComponent<Transform>().localPosition.z;
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
            {
            if (_tempSpread < _spread)
            {
                _crosshairUp.GetComponent<Transform>().localPosition = new Vector3(0, _initialPosition - _tempSpread, 0);
                _crosshairDown.GetComponent<Transform>().localPosition = new Vector3(0, -(_initialPosition - _tempSpread), 0);
                _crosshairLeft.GetComponent<Transform>().localPosition = new Vector3(-(_initialPosition - _tempSpread), 0, 0);
                _crosshairRight.GetComponent<Transform>().localPosition = new Vector3(_initialPosition - _tempSpread, 0, 0);
                _mainCamera.GetComponent<Transform>().localPosition = new Vector3(1, 1, _initialCameraPosition + _tempSpread/20);
                _tempSpread += 0.5f;
            }      
        }
        else
        {
            _crosshairUp.GetComponent<Transform>().localPosition = new Vector3(0, _initialPosition + _spread, 0);
            _crosshairDown.GetComponent<Transform>().localPosition = new Vector3(0, -(_initialPosition + _spread), 0);
            _crosshairLeft.GetComponent<Transform>().localPosition = new Vector3(-(_initialPosition + _spread), 0, 0);
            _crosshairRight.GetComponent<Transform>().localPosition = new Vector3(_initialPosition + _spread, 0, 0);
            _mainCamera.GetComponent<Transform>().localPosition = new Vector3(1, 1, _initialCameraPosition);
            _tempSpread = 1;
        }  
    }
}
