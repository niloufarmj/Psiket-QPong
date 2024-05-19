using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsContrloller : MonoBehaviour
{
    public Image[] optionBoarders;
    public GameObjectStruct[] options;
    int currentSelected = 0;

    [SerializeField] public GameData data;

    void Start()
    {
        optionBoarders[0].enabled = true;
    }

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

    // Update is called once per frame
    void Update()
    {
        UpdateValues();

        UpdateSelection();

        currentSelected = currentSelected % 3;
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == currentSelected % 3)
                optionBoarders[i].enabled = true;
            else
                optionBoarders[i].enabled = false;
        }
    }

    public void ReduceAmount()
    {
        for (int i = 2; i < 5; i++)
        {
            if (i != 0 && !options[currentSelected].cols[i].activeSelf)
            {
                options[currentSelected].cols[i - 1].SetActive(false);
                if (currentSelected == 1)
                {
                    data.aiSpeed -= 1;
                }
                return;
            }
        }
        options[currentSelected].cols[4].SetActive(false);
    }

    public void IncreaseAmount()
    {
        for (int i = 4; i >= 0; i--)
        {
            if (i != 4 && options[currentSelected].cols[i].activeSelf)
            {
                options[currentSelected].cols[i + 1].SetActive(true);

                return;
            }
        }
        
    }

    private void UpdateValues()
    {
        float aiSpeed = 1.7f;
        float ballSpeed = 1.7f;

        for (int i = 1; i < 5; i++)
        {
            if (options[1].cols[i].activeSelf)
                aiSpeed += 0.7f;
            if (options[2].cols[i].activeSelf)
                ballSpeed += 0.7f;
        }

        data.aiSpeed = aiSpeed;
        data.ballSpeed = ballSpeed;
    }
}
