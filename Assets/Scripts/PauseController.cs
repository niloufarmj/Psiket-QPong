using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public Image[] optionBoarders;
    int currentSelected = 0;

    public void MoveUp()
    {
        currentSelected--;

        if (currentSelected < 0)
            currentSelected += 2;
    }

    public void MoveDown()
    {
        currentSelected++;
    }

    public void Select()
    {
        if (currentSelected % 2 == 0)
        {
            GameManager.instance.ResumeGame();
            GameManager.instance.reset = true;
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == currentSelected % 2)
                optionBoarders[i].enabled = true;
            else
                optionBoarders[i].enabled = false;
        }
    }
}
