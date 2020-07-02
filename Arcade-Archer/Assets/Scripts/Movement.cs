using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float groundDistance = 0.4f;
    [SerializeField]
    LayerMask groundMask;

    bool isGrounded;
    bool isDogging;

    [SerializeField]
    float _deadZone = 0.1f;

    bool dodgeAnimationEnd = false;
    float velX;
    float velY;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded)
        {
            Debug.Log("Work!!");
        }

        if (isDogging == false)
        {
            velX = Input.GetAxis("Vertical");
            velY = Input.GetAxis("Horizontal");
        }

        if (Mathf.Abs(velX) < _deadZone) velX = 0f;
        if (Mathf.Abs(velY) < _deadZone) velY = 0f;


        Move(velX, velY);

        lookForKeyDown();

    }

    private void lookForKeyDown()
    {
        if (isGrounded == true && isDogging == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDogging = true;

                Jump();
            }
            if (Input.GetAxis("Jump") > 0)
            {
                isDogging = true;

                Jump();

            }
            if (Input.GetAxis("Fire1") > 0)
            {
                isDogging = true;

                Dodge(velX, velY);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                isDogging = true;
                Dodge(velX, velY);
            }
        }

    }
    void LateUpdate()
    {
        if (dodgeAnimationEnd)
        {
            SetRotationY(0);
            dodgeAnimationEnd = false;
        }
    }

    private void Move(float x, float y)
    {
        anim.SetFloat("velX", x);
        anim.SetFloat("velY", y);
    }

    private void Jump()
    {
        anim.SetTrigger("Jump");
    }

    private void Dodge(float x, float y)
    {
        if (x > -0.8f)
        {
            Vector2 tmp = new Vector2(x, y);
            float angle = Vector2.Angle(tmp, new Vector2(1f, 0f));
            if (angle <= 135)
            {
                if (angle >= 90) angle = 90f;

                if (y >= 0)
                {
                    SetRotationY(angle);
                }
                else
                {
                    SetRotationY(-angle);
                }
            }
        }
        anim.SetTrigger("Dodge");
    }

    private void AnimationDodgeEnd()
    {
        dodgeAnimationEnd = true;
        isDogging = false;
    }

    private void JumpAnimationEnd()
    {
        Debug.Log("wor");
        isDogging = false;
    }

    private void SetRotationY(float angle)
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

}
