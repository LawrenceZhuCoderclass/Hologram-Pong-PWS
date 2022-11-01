using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class GameController : MonoBehaviour
{
    private movement movement;
    private GameState gameState;
    public enum GameState
    {
        Start,
        Playing,
        Paused,
        End
    }

    void Awake()
    {
        GameObject ballObject = GameObject.FindWithTag("Ball");
        if (ballObject != null)
        {
            movement = ballObject.GetComponent<movement>();
        }
        gameState = GameState.Start;
        
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                Time.timeScale = 0;
                //start game
                if (Input.anyKey)
                {
                    Debug.Log("a button was pressed");
                    gameState = GameState.Playing;
                    movement.ResetGame();
                }
                Debug.Log("Start screen");
                break;
            
            case GameState.Playing:
                Debug.Log("Playing the Game");
                Time.timeScale = 1;
                //pause game
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Paused;
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
                Time.timeScale = 0;
                //unpause game
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Playing;
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
}