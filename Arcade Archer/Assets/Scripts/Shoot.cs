using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _arrowSpawn;
    [SerializeField] float _shootForce = 30f;
    [SerializeField] float _reloadTime = 3.0f;

    bool _isArrowLoaded;
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
        Vector3 RayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        if (Input.GetMouseButtonUp(0) && _isArrowLoaded && Physics.Raycast(RayOrigin, _camera.transform.forward, out hit, Mathf.Infinity))
        {
            var _currentlyLoadedArrow = Instantiate(_arrowPrefab, _arrowSpawn.position, Quaternion.LookRotation(hit.point));
            Rigidbody _arrowRigidBody = _currentlyLoadedArrow.GetComponent<Rigidbody>();
            _arrowRigidBody.velocity = (hit.point - transform.position).normalized * _shootForce;
            _isArrowLoaded = false;
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
