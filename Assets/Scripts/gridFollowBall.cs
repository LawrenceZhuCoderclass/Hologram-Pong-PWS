using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridFollowBall : MonoBehaviour
{
    public GameObject ball;
    public bool zAxis;
    Transform ballTransform;

    void Start()
    {
        ballTransform = ball.transform;
    }

    void Update()
    {
        //make new position the ball's position
        if (zAxis)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ballTransform.position.z);
        }
        else
        {
            transform.position = new Vector3(ballTransform.position.x, transform.position.y, transform.position.z);
        }
    }
}