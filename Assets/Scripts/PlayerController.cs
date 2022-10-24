using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private bool Player_1 = false;
    private bool Player_2 = false;
    public GameObject Player;
    float moveHorizontalP1;
    float moveVerticalP1;
    float moveHorizontalP2;
    float moveVerticalP2;
    public bool controllerConnected;
    public bool invertXAxis;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (Player_1 == true) {
            //float moveHorizontalP1 = Input.GetAxis("P1Horizontal");
            //float moveVerticalP1 = Input.GetAxis("P1Vertical");
            if (controllerConnected)
            {
                moveHorizontalP1 = Input.GetAxis("MoveHorizontalP1");
                moveVerticalP1 = Input.GetAxis("MoveVerticalP1");
            }
            else
            {
                moveHorizontalP1 = Input.GetAxis("P1Horizontal");
                moveVerticalP1 = Input.GetAxis("P1Vertical");
            }
            if (invertXAxis)
            {
                moveHorizontalP1 = moveHorizontalP1 * -1;
            }
            Vector3 moveP1 = new Vector3(moveHorizontalP1, moveVerticalP1, 0.0f);
            rb.velocity = moveP1 * speed;
        }
        else if (Player_2 == true){
            //float moveHorizontalP2 = Input.GetAxis("P2Horizontal");        
            //float moveVerticalP2 = Input.GetAxis("P2Vertical");
            if (controllerConnected)
            {
                moveHorizontalP2 = Input.GetAxis("MoveHorizontalP2");
                moveVerticalP2 = Input.GetAxis("MoveVerticalP2");
            }
            else
            {
                moveHorizontalP2 = Input.GetAxis("P2Horizontal");
                moveVerticalP2 = Input.GetAxis("P2Vertical");
            }
            if (invertXAxis)
            {
                moveHorizontalP2 = moveHorizontalP2 * -1;
            }
            Vector3 moveP2 = new Vector3(-moveHorizontalP2, moveVerticalP2, 0.0f);
            rb.velocity = moveP2 * speed;
        }
    }
}
