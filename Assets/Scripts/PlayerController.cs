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
    public bool piramide;
    private string P1Xinput;
    private string P1Yinput;
    private string P2Xinput;
    private string P2Yinput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //checking which player the script is assigned to
        if (Player.tag == "Player_1"){
            Player_1 = true;
        }
        else if (Player.tag == "Player_2"){
            Player_2 = true;
        }

        //checking which settings are true for the movement input
        if (controllerConnected && !piramide)
        {
            P1Xinput = "MoveHorizontalP1";
            P1Yinput = "MoveVerticalP1";

            P2Xinput = "MoveHorizontalP2";
            P2Yinput = "MoveVerticalP2";
        }
        else if (controllerConnected && piramide)
        {
            P1Xinput = "P1HorizontalCoP";
            P1Yinput = "P1VerticalCoP";

            P2Xinput = "P2HorizontalCoP";
            P2Yinput = "P2VerticalCoP";
        }
        else if (!controllerConnected && !piramide)
        {
            P1Xinput = "P1Horizontal";
            P1Yinput = "P1Vertical";

            P2Xinput = "P2Horizontal";
            P2Yinput = "P2Vertical";
        }
        else if (!controllerConnected && piramide)
        {
            P1Xinput = "P1HorizontalKP";
            P1Yinput = "P1VerticalKP";

            P2Xinput = "P2HorizontalKP";
            P2Yinput = "P2VerticalKP";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //updating player movement
        if (Player_1 == true) {
            moveHorizontalP1 = Input.GetAxis(P1Xinput);
            moveVerticalP1 = Input.GetAxis(P1Yinput);
            //when reflected in screen the horizontal movement is flipped
            if (invertXAxis)
            {
                moveHorizontalP1 = moveHorizontalP1 * -1;
            }
            Vector3 moveP1 = new Vector3(moveHorizontalP1, moveVerticalP1, 0.0f);
            rb.velocity = moveP1 * speed;
        }
        else if (Player_2 == true){
            moveHorizontalP2 = Input.GetAxis(P2Xinput);
            moveVerticalP2 = Input.GetAxis(P2Yinput);
            //when reflected in screen the horizontal movement is flipped
            if (invertXAxis)
            {
                moveHorizontalP2 = moveHorizontalP2 * -1;
            }
            Vector3 moveP2 = new Vector3(-moveHorizontalP2, moveVerticalP2, 0.0f);
            rb.velocity = moveP2 * speed;
        }
    }
}
