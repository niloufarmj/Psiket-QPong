using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Shake cameraShake;

    public GameData data;

    public GameObject pauseConvas;

    public Ball ballController;
    public AI paddleController;
    public Player leftPlayerController;
    public Player rightPlayerController;
    public PauseController pauseController;

    public GameObject mobileControls;

    public bool paused = false;
    public bool reset = false;

    private void Start()
    {
        instance = this;
        if (mobileControls)
            mobileControls.SetActive(Application.isMobilePlatform);
    }


    private void Update()
    {
        if (!data.isPvp)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                HandleDown(1);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                HandleUp(1);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                HandleRight(1);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                HandleLeft(1);
            }

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                rightPlayerController.EnableXGate();
            }

            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                rightPlayerController.EnableHGate();
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                HandleDown(1);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                HandleUp(1);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                HandleRight(1);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                HandleLeft(1);
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                rightPlayerController.EnableXGate();
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                rightPlayerController.EnableHGate();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                HandleDown(2);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                HandleUp(2);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                HandleRight(2);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                HandleLeft(2);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                leftPlayerController.EnableXGate();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                leftPlayerController.EnableHGate();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleSelect();
        }
    }


    public void HandleDown(int player)
    {
        if (paused)
        {
            pauseController.MoveDown();
        }
        else
        {
            if (player == 1)
                rightPlayerController.MoveDown();
            else
                leftPlayerController.MoveDown();
        }
    }

    public void HandleUp(int player)
    {
        if (paused)
        {
            pauseController.MoveUp();
        }
        else
        {
            if (player == 1)
                rightPlayerController.MoveUp();
            else
                leftPlayerController.MoveUp();
        }
    }

    public void HandleRight(int player)
    {
        if (!paused)
            if (player == 1)
                rightPlayerController.MoveRight();
            else
                leftPlayerController.MoveRight();
    }

    public void HandleLeft(int player)
    {
        if (!paused)
            if (player == 1)
                rightPlayerController.MoveLeft();
            else
                leftPlayerController.MoveLeft();
    }


    public void HandleSelect()
    {
        if (paused)
            pauseController.Select();
    }


    public void HandlePause()
    {
        if (!ballController.pausedLocked)
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = true;
        ballController.PauseBall();

        if (paddleController)
            paddleController.PausePaddle();
        pauseConvas.SetActive(true);
    }

    public void ResumeGame()
    {
        paused = false;
        ballController.ResumeBall();
        if (paddleController)
            paddleController.ResumePaddle();
        pauseConvas.SetActive(false);
    }

}
