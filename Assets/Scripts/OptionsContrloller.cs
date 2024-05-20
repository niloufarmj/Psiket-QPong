using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsContrloller : MonoBehaviour
{
    public Image[] optionBoarders;
    int currentSelected = 0;

    public GameObject on, off;
    public GameObject[] AIModes;
    public GameObject[] ballModes;

    public TextMeshProUGUI QbitNumber;

    public int AIModeIndex = 1, ballModeIndex = 1;
    [SerializeField] public GameData data;

    void Start()
    {
        if (!Application.isMobilePlatform)
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
            currentSelected += optionBoarders.Length;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();

        if (!Application.isMobilePlatform)
            UpdateSelection();

        currentSelected %= optionBoarders.Length;
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < optionBoarders.Length; i++)
        {
            optionBoarders[i].enabled = (i == currentSelected % optionBoarders.Length);
        }
    }

    public void HandleRight()
    {
        if (currentSelected == 0)
            SwitchSound();
        else if (currentSelected == 1)
            SwitchAIMode(1);
        else if (currentSelected == 2)
            SwitchBallMode(1);
        else
            SwitchQBits();
    }

    public void HandleLeft()
    {
        if (currentSelected == 0)
            SwitchSound();
        else if (currentSelected == 1)
            SwitchAIMode(-1);
        else if (currentSelected == 2)
            SwitchBallMode(-1);
        else
            SwitchQBits();
    }

    public void SwitchSound()
    {
        on.gameObject.SetActive(!on.gameObject.activeSelf);
        off.gameObject.SetActive(!off.gameObject.activeSelf);
    }

    public void SwitchAIMode(int value)
    {
        AIModeIndex += value;
        if (AIModeIndex < 0)
            AIModeIndex += ballModes.Length;

        AIModeIndex %= ballModes.Length;
    }

    public void SwitchBallMode(int value)
    {
        ballModeIndex += value;
        if (ballModeIndex < 0)
            ballModeIndex += ballModes.Length;

        ballModeIndex %= ballModes.Length;
    }

    public void SwitchQBits()
    {
        switch (Convert.ToInt32(QbitNumber.text))
        {
            case 2:
                QbitNumber.text = "3";
                data.Qbits = 3;
                break;
            case 3:
                QbitNumber.text = "2";
                data.Qbits = 2;
                break;
        }
    }

    private void UpdateValues()
    {
        for (int i = 0; i < AIModes.Length; i++)
        {
            AIModes[i].SetActive(false);
            ballModes[i].SetActive(false);
        }
        AIModes[AIModeIndex].SetActive(true);
        ballModes[ballModeIndex].SetActive(true);

        data.aiSpeed = 1.7f + AIModeIndex * 0.7f;
        data.ballSpeed = 1.7f + ballModeIndex * 0.7f;
    }
}
