using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;

    float moveHorizontal;
    float moveVertical;
    public float speed;
    //used when projected as a hologram
    public float XAxismultiplier = 1.0f;
    //used when the player is player2
    private float player2MovementMultiplier = 1.0f;

    //bools for using the correct settings
    public bool controllerConnected;
    public bool piramide;
    private bool isPlayer_1 = false;
    private bool isPlayer_2 = false;

    public GameObject Player;

    //used for input settings
    private string XinputName;
    private string YinputName;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //checking which player the script is assigned to
        if (Player.tag == "Player_1")
        {
            isPlayer_1 = true;
        }
        else if (Player.tag == "Player_2")
        {
            isPlayer_2 = true;
        }

        //checking which settings are true for the movement input
        if (isPlayer_1)
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
        else if (isPlayer_2)
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
        //making sure the player stays withing the boundaries
        Vector3 boundaries = transform.position;
        boundaries.x = Mathf.Clamp(boundaries.x, -3.36f, 3.36f);
        boundaries.y = Mathf.Clamp(boundaries.y, -1.86f, 1.86f);
        transform.position = boundaries;
        //when reflected in screen the horizontal movement is flipped
        moveHorizontal = XAxismultiplier * Input.GetAxis(XinputName);
        moveVertical = Input.GetAxis(YinputName);
        //updating player movement
        Vector3 move = new Vector3(player2MovementMultiplier * moveHorizontal, moveVertical, 0.0f);
        rb.velocity = move * speed;
    }

    public void StartGame()
    {
        //checking which settings are true for the movement input
        if (isPlayer_1)
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
        else if (isPlayer_2)
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
}
