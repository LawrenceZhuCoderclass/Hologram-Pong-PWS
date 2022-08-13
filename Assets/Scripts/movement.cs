using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private Vector3 move;
    private float x;
    private float y;
    private float z;
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
        if (collision.gameObject.tag == "scorewall")
        {
            Debug.Log("this code is for later");
        }
        else
        {
            Debug.Log("collision works");
        }
    }
}
