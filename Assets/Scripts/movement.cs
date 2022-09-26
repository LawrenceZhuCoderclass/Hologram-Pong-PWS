using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class StringExtensions
{
    public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
    public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
}

public class movement : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 3.0f;
    public float speedMultiplier;
    private Vector3 move;
    private float x;
    private float y;
    private float z;
    private Vector3 rotation;
    private float xRot;
    private float yRot;
    private float zRot;
    public GameObject ball;
    public GameObject player1;
    public GameObject player2;
    public TextMeshPro ScoreText;
    public TextMeshProUGUI Scoretext_P1;
    public TextMeshProUGUI ScoreText_P2;
    public int[] counter = new int[2];


    void UpdateScore(TextMeshProUGUI text, int player)
    {
        //score update here
        counter[player] += 1;
        //text.text = counter[player].ToString();
        ScoreText.SetText($"" +
            $"{counter[1].ToString().AddColor(Color.green)}" +
            $"{"-".AddColor(Color.cyan)}" +
            $"{counter[0].ToString().AddColor(Color.magenta)}");
    }

    public void ResetGame()
    {
        counter[0] = 0;
        counter[1] = 0;
        //Scoretext_P1.text = counter[0].ToString();
        //ScoreText_P2.text = counter[1].ToString();
        ScoreText.SetText($"" +
            $"{counter[1].ToString().AddColor(Color.green)}" +
            $"{"-".AddColor(Color.cyan)}" +
            $"{counter[0].ToString().AddColor(Color.magenta)}");
        ResetMovement();
    }
    void ResetMovement()
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
        move = new Vector3 (x, y, z);
        speed = 3.0f;
    }
    void Start()
    {
        //counter[0] = 0;
        //counter[1] = 0;
        rb = ball.GetComponent<Rigidbody>();
        //ResetMovement();
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
            case ("scorewall_p1"):
                UpdateScore(Scoretext_P1, 0);
                ResetMovement();
                break;
            case ("scorewall_p2"):
                Debug.Log("player 2 scored!!!");
                UpdateScore(ScoreText_P2, 1);
                ResetMovement();
                break;

            case ("sidewall"):
                x = x * -1.0f;
                move = new Vector3 (x, y, z);                
                break;

            case ("up_down_wall"):
                y = y * -1.0f;
                move = new Vector3 (x, y, z);
                break;

            case ("Player_1"):
                z = z * -1.0f;
                x = ((rb.position.x - player1.transform.position.x) + x) / 2;
                y = ((rb.position.x - player1.transform.position.y) + y) / 2;
                move = new Vector3(x, y, z);
                speed = speed + 0.2f;
                break;

            case ("Player_2"):
                z = z * -1.0f;
                x = ((rb.position.x - player2.transform.position.x) + x) / 2;
                y = ((rb.position.x - player2.transform.position.y) + y) / 2;
                move = new Vector3(x, y, z);
                speed = speed + 0.2f;
                break;

            default:
                z = z * -1.0f;
                move = new Vector3 (x, y, z);
                speed = speed + 0.5f; 
                break;
        }
        
        Debug.Log("leaving the collision");
    }
}

