using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _playerWalkingSpeed = 5f;

    float _verticalRotationLimit = 90f;
    float _verticalRotation = 0;
    float _forwardMovement;
    float _sidewaysMovement;
    float _verticalVelocity;

    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRotationLimit, _verticalRotationLimit);
        _verticalRotation -= Input.GetAxis("Mouse Y");
        Camera.main.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);

        _forwardMovement = Input.GetAxis("Vertical") * _playerWalkingSpeed;
        _sidewaysMovement = Input.GetAxis("Horizontal") * _playerWalkingSpeed;
        Vector3 playerMovement = new Vector3(_sidewaysMovement, _verticalVelocity, _forwardMovement);
        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }
}
