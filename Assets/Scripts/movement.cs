using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class movement : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 3.0f;
    public float speedMultiplier;
    private Vector3 move;
    private float x;
    private float y;
    private float z;
    public GameObject ball;
    public TextMeshProUGUI Scoretext_P1;
    public TextMeshProUGUI ScoreText_P2;
    public int[] counter = new int[2];


    void UpdateScore(TextMeshProUGUI text, int player)
    {
        //score update here
        counter[player] += 1;
        text.text = counter[player].ToString();
    }

    public void ResetGame()
    {
        counter[0] = 0;
        counter[1] = 0;
        Scoretext_P1.text = counter[0].ToString();
        ScoreText_P2.text = counter[1].ToString();
        ResetMovement();
    }
    void ResetMovement()
    {
        ball.GetComponent<Transform>().position = new Vector3(0.67f, -0.39f, 0.72f);
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
        counter[0] = 0;
        counter[1] = 0;
        rb = ball.GetComponent<Rigidbody>();
        ResetMovement();
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

            default:
                z = z * -1.0f;
                move = new Vector3 (x, y, z);
                speed = speed + 0.5f; 
                break;
        }
        
        Debug.Log("leaving the collision");
    }
}

