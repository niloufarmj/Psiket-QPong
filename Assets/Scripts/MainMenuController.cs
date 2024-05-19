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

    bool locked = false;


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
            SceneManager.LoadScene("Game");
        }

        if (currentSelected % 3 == 1)
        {
            UIManager.instance.HandleOptions();
        }

        if (currentSelected % 3 == 2)
        {
            creditsMenu.SetActive(true);
        }
    }

    void Update()
    {
        UpdateSelection();
    }

    void UpdateSelection()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == currentSelected % 3)
                selectedButtons[i].gameObject.SetActive(true);
            else
                selectedButtons[i].gameObject.SetActive(false);
        }
    }
}
