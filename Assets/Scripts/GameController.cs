using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class GameController : MonoBehaviour
{
    private movement movement;
    private GameState gameState;
    public GameObject Field;
    public GameObject StartText;
    public GameObject OptionsText;
    public GameObject PauseText;
    private GameObject ballObject;
    public enum GameState
    {
        Start,
        Playing,
        Paused,
        Options,
        End
    }

    void Awake()
    {
        //nothing here for now        
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                //start game
                if (Input.GetKeyDown("space"))
                {                   
                    gameState = GameState.Playing;
                    StartText.SetActive(false);
                    Field.SetActive(true);
                    SearchBall();
                    movement.ResetGame();
                    
                }
                else if (Input.GetKeyDown("o"))
                {
                    gameState = GameState.Options;
                    StartText.SetActive(false);
                    OptionsText.SetActive(true);
                }
                else if (Input.GetKeyDown("q"))
                {
                    gameState = GameState.End;
                }
                break;
            
            case GameState.Options:
                if (Input.GetKeyDown("h"))
                {
                    //controls to that of the holofil
                }
                else if (Input.GetKeyDown("p"))
                {
                    //controls to that of the pepper's cone
                }
                else if (Input.GetKeyDown("n"))
                {
                    //return to the normal controls
                }
                else if (Input.GetKeyDown("e"))
                {
                    gameState = GameState.Start;
                    OptionsText.SetActive(false);
                    StartText.SetActive(true);
                }
                break; 
            case GameState.Playing:
                Time.timeScale = 1;
                //pause game
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Paused;
                    Field.SetActive(false);
                    PauseText.SetActive(true); 
                }
                //check for winner
                if (movement.counter[0] == 3)
                {
                    movement.winner = 1;
                    movement.ball.SetActive(false);
                    movement.ScoreText.SetText($"" +
                $"{"Winner".AddColor(Color.green)}");
                    gameState = GameState.End;
                }
                else if (movement.counter[1] == 3)
                {
                    movement.winner = 2;
                    movement.ball.SetActive(false);
                    movement.ScoreText.SetText($"" +
                $"{"Winner".AddColor(Color.magenta)}");
                    gameState = GameState.End;
                }
                break;
            
            case GameState.Paused:
                Debug.Log("pausing the game");
                //Time.timeScale = 0;
                //unpause game
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Playing;
                    PauseText.SetActive(false);
                    Field.SetActive(true);
                    SearchBall();
                }
                //restart game
                if (Input.GetKeyDown("q"))
                {
                    gameState = GameState.Start;
                    movement.ResetGame();
                }
                break;
            
            case GameState.End:
                Debug.Log("The game is over");
                //Time.timeScale = 0;
                //restart game
                if (Input.GetKeyDown("space"))
                {
                    gameState = GameState.Playing;
                    movement.ResetGame();
                }
                break;
        }
    }
    void SearchBall()
    {
        ballObject = GameObject.FindWithTag("Ball");
        if (ballObject != null)
        {
            movement = ballObject.GetComponent<movement>();
        }        
    }
}