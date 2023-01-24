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

    public AudioSource WinSound;
    public AudioSource Select;

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
                gameStateStart();
                break;
            
            case GameState.Options:
                gameStateOptions();
                break;

            case GameState.Playing:
                gameStatePlaying();
                break;
            
            case GameState.Paused:
                gameStatePaused();
                break;
            
            case GameState.End:
                gameStateEnd();
                break;
        }
    }

    public void EndGame()
    {
        WinSound.Play();
        gameState = GameState.End;
    }

    private void SearchBall()
    {
        ballObject = GameObject.FindWithTag("Ball");
        if (ballObject != null)
        {
            movement = ballObject.GetComponent<movement>();
        }        
    }

    private void gameStateStart()
    {
        //During the Start screen
        if (Input.GetKeyDown("space"))
        {
            //start the game
            gameState = GameState.Playing;
            StartText.SetActive(false);
            Field.SetActive(true);
            SearchBall();
            movement.ResetGame();
            if (Invert_Axis == true)
            {
                //mirror the horizontal movement of the players if a hologram is used
                PlayerController_1.XAxismultiplier = -1.0f;
                PlayerController_2.XAxismultiplier = -1.0f;
            }
            if (Piramid == true)
            {
                PlayerController_1.piramide = true;
                PlayerController_2.piramide = true;
                //use the correct camera's
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
            //switch to options menu
            Select.Play();
            gameState = GameState.Options;
            StartText.SetActive(false);
            OptionsText.SetActive(true);
        }
        else if (Input.GetKeyDown("q"))
        {
            //quit the game
            Application.Quit();
        }
    }

    private void gameStateOptions()
    {
        //During the options screen
        if (Input.GetKeyDown("h"))
        {
            Select.Play();
            Invert_Axis = true;
            mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 90);
            Screen.SetResolution(720, 1334, true);
            //controls to that of the holofil
        }
        else if (Input.GetKeyDown("p"))
        {
            Select.Play();
            Invert_Axis = true;
            Piramid = true;
            mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0);
            //controls to that of the pyramid
        }
        else if (Input.GetKeyDown("n"))
        {
            Select.Play();
            Invert_Axis = false;
            Piramid = false;
            Controller = false;
            mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0);
            //return to the normal controls
        }
        else if (Input.GetKeyDown("c"))
        {
            Select.Play();
            Controller = true;
        }
        else if (Input.GetKeyDown("e"))
        {
            Select.Play();
            gameState = GameState.Start;
            OptionsText.SetActive(false);
            StartText.SetActive(true);
        }
    }

    private void gameStatePlaying()
    {
        //Things that can be done while playing
        Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.Paused;
            Field.SetActive(false);
            PauseText.SetActive(true);
        }
    }

    private void gameStatePaused()
    {
        //During the pause screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //unpause game
            gameState = GameState.Playing;
            PauseText.SetActive(false);
            Field.SetActive(true);
            SearchBall();
        }
        if (Input.GetKeyDown("e"))
        {
            //restart game
            gameState = GameState.Start;
            StartText.SetActive(true);
            Field.SetActive(false);
            PauseText.SetActive(false);
            movement.ResetGame();
        }
    }

    private void gameStateEnd()
    {
        //restart game after game over
        if (Input.GetKeyDown("space"))
        {
            gameState = GameState.Playing;
            movement.ResetGame();
        }
    }
}