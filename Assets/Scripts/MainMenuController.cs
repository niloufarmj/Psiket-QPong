using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public Button[] selectedButtons;
    int currentSelected = 0;

    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject startMenu;


    public void MoveDown()
    {
        currentSelected++;
    }

    public void MoveUp()
    {
        currentSelected--;

        if (currentSelected < 0)
            currentSelected += 3;
    }

    public void Select() 
    {
        if (currentSelected % 3 == 0)
        {
            StartGame();
        }

        if (currentSelected % 3 == 1)
        {
            UIManager.instance.HandleOptions();
        }

        if (currentSelected % 3 == 2)
        {
            ShowCredits();
        }
    }

    public void StartGame()
    {
        if (Application.isMobilePlatform)
        {
            UIManager.instance.data.isPvp = false;
            if (UIManager.instance.data.Qbits == 2)
                SceneManager.LoadScene("Game 2Bits");
            else
                SceneManager.LoadScene("Game 3Bits");
        }

        else
        {
            UIManager.instance.mainMenuLocked = true;
            startMenu.SetActive(true);
        }
    }

    public void ShowCredits()
    {
        creditsMenu.SetActive(true);
        UIManager.instance.mainMenuLocked = true;
    }

    void Update()
    {
        if (!Application.isMobilePlatform)
        {
            UpdateSelection();
        }
    }

    void UpdateSelection()
    {
        for (int i = 0; i < 3; i++)
        {
            selectedButtons[i].gameObject.SetActive((i == currentSelected % 3));
        }
    }
}
