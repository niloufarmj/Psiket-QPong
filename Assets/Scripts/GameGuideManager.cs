using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGuideManager : MonoBehaviour
{
    public static GameGuideManager instance;

    public Player rightPlayerController;
    public Player leftPlayerController;

    public GameObject mobileControls;
    public GameObject pcGuide;

    public bool isPvp;

    private void Start()
    {
        instance = this;
        if (mobileControls)
            mobileControls.SetActive(Application.isMobilePlatform);
        pcGuide.SetActive(!Application.isMobilePlatform);
    }


    private void Update()
    {
        if (!isPvp)
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
            Return();
        }

    }


    public void HandleDown(int player)
    {
        if (player == 1)
            rightPlayerController.MoveDown();
        else
            leftPlayerController.MoveDown();
    }

    public void HandleUp(int player)
    {

        if (player == 1)
            rightPlayerController.MoveUp();
        else
            leftPlayerController.MoveUp();
    }

    public void HandleRight(int player)
    {
        if (player == 1)
            rightPlayerController.MoveRight();
        else
            leftPlayerController.MoveRight();
    }

    public void HandleLeft(int player)
    {
        if (player == 1)
            rightPlayerController.MoveLeft();
        else
            leftPlayerController.MoveLeft();
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

}
