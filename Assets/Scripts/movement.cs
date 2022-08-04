using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = new Vector3 (Random.Range(-1,1), Random.Range(-1,1), Random.Range(-1,1));
        Debug.Log(move);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = move * speed;
    }
}
