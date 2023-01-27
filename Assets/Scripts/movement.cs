using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class StringExtensions
{
    //text color for score
    public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
    public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
}

public class movement : MonoBehaviour
{
    private Rigidbody rb;

    private float speed = 3.0f;
    public float speedMultiplier;
    private float x;
    private float y;
    private float z;

    private Vector3 move;

    public GameObject ball;
    public GameObject player1;
    public GameObject player2;

    public TextMeshPro ScoreText;

    public int[] counter = new int[2];

    public GameController gameController;

    public cameraRotator rotateCameraScript;

    public int winner;

    public AudioSource Bounce;
    public AudioSource Wall;
    public AudioSource Score;

    void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
        ResetGame();
    }

    void FixedUpdate()
    {
        rb.velocity = move * speed * speedMultiplier;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //player scored
            case ("scorewall_p1"):
                Score.Play();
                UpdateScore(1);
                ResetMovement();
                break;
            case ("scorewall_p2"):
                Score.Play();
                UpdateScore(0);
                ResetMovement();
                break;
            //bounce off wall
            case ("sidewall"):
                x = x * -1.0f;
                move = new Vector3 (x, y, z);
                Wall.Play();              
                break;
            case ("up_down_wall"):
                y = y * -1.0f;
                move = new Vector3 (x, y, z);
                Wall.Play();   
                break;
            default:
                z = z * -1.0f;
                move = new Vector3 (x, y, z);
                speed = speed + 0.5f; 
                break;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            //bounce off player
            case ("Player_1"):
                Bounce.Play();
                z = z * -1.0f;
                x = ((rb.position.x - player1.transform.position.x) + x) / 2;
                y = ((rb.position.y - player1.transform.position.y) + y) / 2;
                move = new Vector3(x, y, z);
                speed = speed + 0.75f;
                rotateCameraScript.player1 = false;
                break;
            case ("Player_2"):
                Bounce.Play();
                z = z * -1.0f;
                x = ((rb.position.x - player2.transform.position.x) + x) / 2;
                y = ((rb.position.y - player2.transform.position.y) + y) / 2;
                move = new Vector3(x, y, z);
                speed = speed + 0.75f;
                rotateCameraScript.player1 = true;
                break;

            default:
                z = z * -1.0f;
                move = new Vector3(x, y, z);
                speed = speed + 0.5f;
                break;
        }
    }

    void UpdateScore(int player)
    {
        //score update here
        counter[player] += 1;
        //check for winner
        if (counter[0] == 3)
        {
            winner = 1;
            ball.SetActive(false);
            ScoreText.SetText($"" +
        $"{"Winner".AddColor(Color.green)}");
            gameController.EndGame();
        }
        else if (counter[1] == 3)
        {
            winner = 2;
            ball.SetActive(false);
            ScoreText.SetText($"" +
        $"{"Winner".AddColor(Color.magenta)}");
            gameController.EndGame();
        }
        //update the text based on who has won
        if (winner == 0)
        {
            ScoreText.SetText($"" +
                $"{counter[0].ToString().AddColor(Color.green)}" +
                $"{"-".AddColor(Color.cyan)}" +
                $"{counter[1].ToString().AddColor(Color.magenta)}");
        }
        else if (winner == 1)
        {
            ScoreText.SetText($"" +
                $"{"Winner".AddColor(Color.green)}");
        }
        else if (winner == 2)
        {
            ScoreText.SetText($"" +
                $"{"Winner".AddColor(Color.magenta)}");
        }
    }

    public void ResetGame()
    {
        //reset the score count and the winner
        counter[0] = 0;
        counter[1] = 0;
        winner = 0;
        ScoreText.SetText($"" +
            $"{counter[0].ToString().AddColor(Color.green)}" +
            $"{"-".AddColor(Color.cyan)}" +
            $"{counter[1].ToString().AddColor(Color.magenta)}");
        ball.SetActive(true);
        ResetMovement();
    }

    void ResetMovement()
    {
        //reset the movement of the ball
        if (winner == 0)
        {
            ball.GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
            x = Random.Range(-1.0f, 1.0f);
            y = Random.Range(-1.0f, 1.0f);
            if (Random.value > 0.5f)
            {
                z = 1.0f;
                rotateCameraScript.player1 = false;
            }
            else
            {
                z = -1.0f;
                rotateCameraScript.player1 = true;
            }
            move = new Vector3(x, y, z);
            speed = 3.0f;
        }
        else {
            ball.SetActive(false);
        }
    }
}

