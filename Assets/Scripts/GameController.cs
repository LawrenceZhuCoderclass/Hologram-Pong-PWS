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
    public PlayerController PlayerController_1;
    public PlayerController PlayerController_2;
    public bool Invert_Axis;
    public bool Piramid;
    private bool Controller;
    public GameObject HologramCam;
    public GameObject RotatingCam;
    public GameObject mainCamera;

    public enum GameState
    {
        Start,
        Playing,
        Paused,
        Options,
        End
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
                    if(Invert_Axis == true)
                    {
                        PlayerController_1.invertXAxis = true;
                        PlayerController_2.invertXAxis = true;
                    }
                    if (Piramid == true)
                    {
                        PlayerController_1.piramide = true;
                        PlayerController_2.piramide = true;
                        RotatingCam.SetActive(false);
                        HologramCam.SetActive(true);
                    }
                    if (Controller == true)
                    {
                        PlayerController_1.controllerConnected = true;
                        PlayerController_2.controllerConnected = true;
                    }
                    
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
                    Invert_Axis = true;
                    mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 90);
                    Screen.SetResolution(720, 1334, true);
                    //controls to that of the holofil
                }
                else if (Input.GetKeyDown("p"))
                {
                    Invert_Axis = true;
                    Piramid = true;
                    mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0);
                    //controls to that of the pepper's cone
                }
                else if (Input.GetKeyDown("n"))
                {
                    Invert_Axis = false;
                    Piramid = false;
                    Controller = false;
                    mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0);
                    //return to the normal controls
                }
                else if (Input.GetKeyDown("c"))
                {
                    Controller = true;
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