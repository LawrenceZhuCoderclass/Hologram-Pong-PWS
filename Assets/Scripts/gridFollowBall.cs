using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridFollowBall : MonoBehaviour
{
    public GameObject ball;
    Transform ballTransform;

    void Start()
    {
        ballTransform = ball.transform;
    }

    void FixedUpdate()
    {
        //follow the ball on the z axis
        transform.position = new Vector3(transform.position.x, transform.position.y, ballTransform.position.z);
    }
}