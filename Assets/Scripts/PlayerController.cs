using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private BoxCollider boxcollider;
    private bool Player_1 = false;
    private bool Player_2 = false;
    public GameObject Player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxcollider = transform.GetComponent<BoxCollider>();
        if (Player.tag == "Player_1"){
            Player_1 = true;
        }
        else if (Player.tag == "Player_2"){
            Player_2 = true;
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player_1 == true){
            float moveHorizontalP1 = Input.GetAxis("P1Horizontal");
            float moveVerticalP1 = Input.GetAxis("P1Vertical");
            Vector3 moveP1 = new Vector3(moveHorizontalP1, moveVerticalP1, 0.0f);
            rb.velocity = moveP1 * speed;
        }
        else if (Player_2 == true){
            float moveHorizontalP2 = Input.GetAxis("P2Horizontal");        
            float moveVerticalP2 = Input.GetAxis("P2Vertical");
            Vector3 moveP2 = new Vector3(moveHorizontalP2, moveVerticalP2, 0.0f);
            rb.velocity = moveP2 * speed;
        }
        

        
       

    }
}
