using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float _arrowLifeTime;

    Rigidbody _arrowRigidBody;
    void Start()
    {
        _arrowRigidBody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(_arrowRigidBody.velocity);
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_arrowRigidBody.velocity);
        _arrowLifeTime -= Time.deltaTime;
        if (_arrowLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Arrow") || !collision.collider.CompareTag("Enemy"))
        {
            _arrowRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<>
        }
    }
}
