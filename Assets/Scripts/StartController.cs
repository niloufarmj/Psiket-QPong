using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public Image[] optionBoarders;
    int currentSelected = 0;

    public void MoveDown()
    {
        currentSelected++;
    }

    public void MoveUp()
    {
        currentSelected--;

        if (currentSelected < 0)
            currentSelected += optionBoarders.Length;
    }

    public void Select()
    {
        if (currentSelected % optionBoarders.Length == 0)
        {
            UIManager.instance.data.isPvp = false;
            if (UIManager.instance.data.Qbits == 2)
                SceneManager.LoadScene("Game 2Bits");
            else
                SceneManager.LoadScene("Game 3Bits");
        }
        else
        {
            UIManager.instance.data.isPvp = true;
            if (UIManager.instance.data.Qbits == 2)
                SceneManager.LoadScene("Game 2Bits PVP");
            else
                SceneManager.LoadScene("Game 3Bits PVP");
        }
    }

    void Update()
    {
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < optionBoarders.Length; i++)
        {
            optionBoarders[i].enabled = (i == currentSelected % optionBoarders.Length);
        }
    }
}
