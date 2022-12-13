using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 eulerAngleVelocity;
    public GameController gameController;
    public cameraRotator cameraRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);
    }

    void FixedUpdate()
    {
        //rotate the score
        if (gameController.Piramid)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        else if (gameController.Invert_Axis)
        {
            transform.eulerAngles = new Vector3(0.0f, cameraRotation.transform.eulerAngles.y + 180f, 0.0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, cameraRotation.transform.eulerAngles.y, 0.0f);
        }
    }
}
