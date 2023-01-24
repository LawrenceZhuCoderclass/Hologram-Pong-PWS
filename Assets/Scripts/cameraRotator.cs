using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotator : MonoBehaviour
{
    public bool player1;
    //camera angles for player 1 and player 2 view
    public Quaternion to1;
    public Quaternion to2;
    private float speed = 4.0f;

    private void Start()
    {
        //set the right angles for the camera for each player
        to1 = Quaternion.AngleAxis(180, transform.up);
        to2 = Quaternion.AngleAxis(0, transform.up);
        player1 = false;
    }

    private void FixedUpdate()
    {
        //rotate to the right player
        if (!player1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, to1, speed * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, to2, speed * Time.fixedDeltaTime);
        }
    }
}