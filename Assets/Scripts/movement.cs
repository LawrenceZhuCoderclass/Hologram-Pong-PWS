using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 3.0f;
    private Vector3 move;
    private float x;
    private float y;
    private float z;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        x = Random.Range(-1.0f, 1.0f);
        y = Random.Range(-1.0f, 1.0f);
        if (Random.value > 0.5f)
        {
            z = 1.0f;  
        }
        else
        {
            z = -1.0f; 
        }
        move = new Vector3 (x, y, z);
        Debug.Log(move);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = move * speed;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision is happening");
        switch (collision.gameObject.tag)
        {
            case ("scorewall"):
                Debug.Log("this is for the score system");
                //change the score here
                ball.GetComponent<Transform>().position = new Vector3(0.67f, -0.39f, 0.72f);
                speed = 3.0f;
                // Destroy(ball);
                // Instantiate(ball);
                break;
            case ("sidewall"):
                x = x * -1.0f;
                Debug.Log("this is the value of x" + x);
                move = new Vector3 (x, y, z);                
                break;

            case ("up_down_wall"):
                y = y * -1.0f;
                Debug.Log("this is the value of y" + y);
                move = new Vector3 (x, y, z);
                break;

            default:
                z = z * -1.0f;
                Debug.Log("this is the value of z" + z);
                move = new Vector3 (x, y, z);
                speed = speed + 0.5f; 
                break;
        }
        
        Debug.Log("leaving the collision");
    }
}
