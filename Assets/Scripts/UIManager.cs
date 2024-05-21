using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameData data;

    public MainMenuController mainMenuController;
    public OptionsContrloller optoinController;
    public StartController startController;

    public GameObject optionsCanvas, creditsCanvas, startCanvas;

    public GameObject optionsSelected, creditsSelected;
    public bool mainMenuLocked = false;

    public Image[] optionBoarders;

    private void Start()
    {
        instance = this;
        data.aiSpeed = 3.1f;
        data.ballSpeed = 2.4f;
        data.Qbits = 3;
        optionsSelected.SetActive(Application.isMobilePlatform);
        creditsSelected.SetActive(Application.isMobilePlatform);

        for (int i = 0; i < optionBoarders.Length; i++)
            optionBoarders[i].enabled = !Application.isMobilePlatform;
    }


    private void Update()
    {

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

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
        {
            HandleSelect();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleReturn();
        }

    }

    public void HandleOptions()
    {
        optionsCanvas.SetActive(true);
        mainMenuLocked = true;
    }

    public void HandleCredits()
    {
        creditsCanvas.SetActive(true);
        mainMenuLocked = true;
    }

    public void HandleDown()
    {
        if (!mainMenuLocked)
        {
            mainMenuController.MoveDown();
        }
        else
        {
            if (optionsCanvas.activeSelf)
            {
                optoinController.MoveDown();
            }
            else if (startCanvas.activeSelf)
            {
                startController.MoveDown();
            }
        }
    }

    public void HandleUp()
    {
        if (!mainMenuLocked)
        {
            mainMenuController.MoveUp();
        }
        else
        {
            if (optionsCanvas.activeSelf)
            {
                optoinController.MoveUp();
            }
            else if (startCanvas.activeSelf)
            {
                startController.MoveUp();
            }
        }
    }

    public void HandleRight()
    {
        if (optionsCanvas.activeSelf)
        {
            optoinController.HandleRight();
        }
    }

    public void HandleLeft()
    {
        if (optionsCanvas.activeSelf)
        {
            optoinController.HandleLeft();
        }
    }

    public void HandleSelect()
    {
        if (!mainMenuLocked)
        {
            mainMenuController.Select();
        }
        else
        {
            if (startCanvas.activeSelf)
            {
                startController.Select();
            }
        }
    }

    public void HandleReturn()
    {
        if (mainMenuLocked)
        {
            optionsCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
            startCanvas.SetActive(false);
            mainMenuLocked = false;
        }
    }
}
