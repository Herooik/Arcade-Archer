using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    float _deadZone = 0.1f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        float velX = Input.GetAxis("Vertical");
        float velY = Input.GetAxis("Horizontal");

        if(Mathf.Abs(velX) < _deadZone) velX=0f;
        if(Mathf.Abs(velY) < _deadZone) velY=0f;

        Move(velX, velY);
    }

    private void Move(float x, float y) 
    {
        anim.SetFloat("velX", x);
        anim.SetFloat("velY", y);
    }
}
