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
    private float xRot;
    private float yRot;
    private float zRot;

    private Vector3 move;

    private Vector3 rotation;

    public GameObject ball;
    public GameObject player1;
    public GameObject player2;

    public TextMeshPro ScoreText;

    public TextMeshProUGUI Scoretext_P1;
    public TextMeshProUGUI ScoreText_P2;

    public int[] counter = new int[2];

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
        //decide where the camera should be for a rotating camera
        if (rb.velocity.z < 0)
        {
            rotateCameraScript.player1 = true;
        }
        else if (rb.velocity.z > 0)
        {
            rotateCameraScript.player1 = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //player scored
            case ("scorewall_p1"):
                UpdateScore(Scoretext_P1, 1);
                ResetMovement();
                Score.Play();
                break;
            case ("scorewall_p2"):
                UpdateScore(ScoreText_P2, 0);
                ResetMovement();
                Score.Play();
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
                move = new Vector3 (x, y, z);
                speed = speed + 0.5f; 
                break;
        }
    }
    
    void UpdateScore(TextMeshProUGUI text, int player)
    {
        //score update here
        counter[player] += 1;
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
            }
            else
            {
                z = -1.0f;
            }
            move = new Vector3(x, y, z);
            speed = 3.0f;
        }
        else {
            ball.SetActive(false);
        }
    }
}

