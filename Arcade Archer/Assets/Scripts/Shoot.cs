using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _arrowSpawn;
    [SerializeField] float _maxShootForce = 30f;
    [SerializeField] float _reloadTime = 2.0f;
    [SerializeField] float _maxStretchTime = 3.0f;

    bool _isArrowLoaded;
    bool _isReadyToShoot;
    float _shootForce;
    float _currentStretchTime;
    RaycastHit hit;

    [SerializeField] Text _tempText;
    void Awake()
    {
        _isArrowLoaded = true;
        _tempText.text = "READY";
        _tempText.color = Color.green;    
    }

    void Update()
    {
        Vector3 RayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        if (Input.GetButton("Fire1") && _isArrowLoaded && _currentStretchTime <= _maxStretchTime && Physics.Raycast(RayOrigin, _camera.transform.forward, out hit, Mathf.Infinity))
        {
            _shootForce += Time.deltaTime * 50.0f;
            Debug.Log(_shootForce);
            _isReadyToShoot = true;
            _currentStretchTime += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0) && _isReadyToShoot)
        {
            var _currentlyLoadedArrow = Instantiate(_arrowPrefab, _arrowSpawn.position, Quaternion.LookRotation(hit.point));
            Rigidbody _arrowRigidBody = _currentlyLoadedArrow.GetComponent<Rigidbody>();
            _arrowRigidBody.velocity = (hit.point - transform.position).normalized * _shootForce;
            _isArrowLoaded = false;
            _isReadyToShoot = false;
            _shootForce = 0.0f;
            _currentStretchTime = 0.0f;
            Reload();
        }
    }
    void Reload()
    {
        StartCoroutine("ReloadBow");
    }

    IEnumerator ReloadBow()
    {
        _tempText.text = "REALOADING";
        _tempText.color = Color.red;
        yield return new WaitForSeconds(_reloadTime);
        _isArrowLoaded = true;
        _tempText.text = "READY";
        _tempText.color = Color.green;
    }
}
