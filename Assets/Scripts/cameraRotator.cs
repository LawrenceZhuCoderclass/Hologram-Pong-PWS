using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotator : MonoBehaviour
{
    //private Rigidbody rb;
    //private Vector3 EulerAngleVelocity;
    private float timer;
    public bool player1;
    public Quaternion to;
    public Quaternion to1;
    public Quaternion to2;
    private float speed = 4.0f;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //EulerAngleVelocity = new Vector3(0, 180, 0);
        //rotating = false;
        timer = 0.0f;
        to1 = Quaternion.AngleAxis(180, transform.up);
        to2 = Quaternion.AngleAxis(0, transform.up);
        player1 = false;
    }

    private void FixedUpdate()
    {
        if (!player1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, to1, speed * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, to2, speed * Time.fixedDeltaTime);
        }
        //if (rotating)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, to, speed * speed);
        //    timer = timer + Time.deltaTime;
        //}
        //else
        //{
        //    timer = 0.0f;
        //    rotating = false;
        //}
    }

    //public void RotateCamera(float y)
    //{
    //    EulerAngleVelocity = new Vector3(0, y, 0);
    //}
}