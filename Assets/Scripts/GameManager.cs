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
    public Player playerController;
    public PauseController pauseController;

    public GameObject mobileControls;

    public bool paused = false;
    public bool reset = false;

    private void Start()
    {
        instance = this;
        mobileControls.SetActive(Application.isMobilePlatform);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            HandleDown();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            HandleUp();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            HandleRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            HandleLeft();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleSelect();
        }

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
        paddleController.PausePaddle();
        pauseConvas.SetActive(true);
    }

    public void ResumeGame()
    {
        paused = false;
        ballController.ResumeBall();
        paddleController.ResumePaddle();
        pauseConvas.SetActive(false);
    }

    public void HandleDown()
    {
        if (paused)
        {
            pauseController.MoveDown();
        }
        else
        {
            playerController.MoveDown();
        }
    }

    public void HandleUp()
    {
        if (paused)
        {
            pauseController.MoveUp();
        }
        else
        {
            playerController.MoveUp();
        }
    }

    public void HandleRight()
    {
        if (!paused)
            playerController.MoveRight();
    }

    public void HandleLeft()
    {
        if (!paused)
            playerController.MoveLeft();
    }

    public void HandleSelect()
    {
        if (paused)
            pauseController.Select();
    }

}
