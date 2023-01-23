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

    float moveHorizontal;
    float moveVertical;
    
    public bool controllerConnected;
    //public bool invertXAxis;
    public float XAxismultiplier = 1.0f;
    private float player2MovementMultiplier = 1.0f;
    public bool piramide;

    private string XinputName;
    private string YinputName;

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
        if (Player_1)
        {
            if (controllerConnected && !piramide)
            {
                XinputName = "MoveHorizontalP1";
                YinputName = "MoveVerticalP1";
            }
            else if (controllerConnected && piramide)
            {
                XinputName = "P1HorizontalCoP";
                YinputName = "P1VerticalCoP";
            }
            else if (!controllerConnected && !piramide)
            {
                XinputName = "P1Horizontal";
                YinputName = "P1Vertical";
            }
            else if (!controllerConnected && piramide)
            {
                XinputName = "P1HorizontalKP";
                YinputName = "P1VerticalKP";
            }
        }
        else if (Player_2)
        {
            player2MovementMultiplier = -1;
            if (controllerConnected && !piramide)
            {
                XinputName = "MoveHorizontalP2";
                YinputName = "MoveVerticalP2";
            }
            else if (controllerConnected && piramide)
            {
                XinputName = "P2HorizontalCoP";
                YinputName = "P2VerticalCoP";
            }
            else if (!controllerConnected && !piramide)
            {
                XinputName = "P2Horizontal";
                YinputName = "P2Vertical";
            }
            else if (!controllerConnected && piramide)
            {
                XinputName = "P2HorizontalKP";
                YinputName = "P2VerticalKP";
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 boundaries = transform.position;
        boundaries.x = Mathf.Clamp(boundaries.x, -3.36f, 3.36f);
        boundaries.y = Mathf.Clamp(boundaries.y, -1.86f, 1.86f);
        transform.position = boundaries;
        //updating player movement
            //when reflected in screen the horizontal movement is flipped
            moveHorizontal = XAxismultiplier * Input.GetAxis(XinputName);
            moveVertical = Input.GetAxis(YinputName);
            Vector3 move = new Vector3(player2MovementMultiplier * moveHorizontal, moveVertical, 0.0f);
            rb.velocity = move * speed;
    }
}
