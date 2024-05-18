using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObjectStruct[] selections;
    public GameObjectStruct[] hGates;
    public GameObjectStruct[] xGates;
    public GameObject[] finalPaddles;
    public GameObject[] prePaddles;

    int currentRow = 0;
    int currentCol = 0;

    public Transform ballTransform;
    
    private void EnableSelection(int i, int j)
    {
        for (int s = 0; s < 3; s++)
        {
            for (int k = 0; k < 10; k++)
            {
                selections[s].cols[k].SetActive(false);
            }
        }
        selections[i].cols[j].SetActive(true);
    }

    private void EnableHGate(int i, int j)
    {
        xGates[i].cols[j].SetActive(false);
        hGates[i].cols[j].SetActive(!hGates[i].cols[j].activeSelf);
    }

    private void EnableXGate(int i, int j)
    {
        hGates[i].cols[j].SetActive(false);
        xGates[i].cols[j].SetActive(!xGates[i].cols[j].activeSelf);
    }

    private void EnableFinalPaddle(int index)
    {
        finalPaddles[index].SetActive(true);
    }

    private void EnablePrePaddle(int index)
    {
        
        prePaddles[index].SetActive(true);
    }

    private void UpdatePaddle()
    {
        

        int xRow1 = 0;
        int xRow2 = 0;
        int xRow3 = 0;
        int hRow1 = 0;
        int hRow2 = 0;
        int hRow3 = 0;
        int finalResult = 0;

        for (int i = 0; i < 10; i++)
        {
            if (xGates[0].cols[i].activeSelf)
                xRow1 += 1;
            if (xGates[1].cols[i].activeSelf)
                xRow2 += 1;
            if (xGates[2].cols[i].activeSelf)
                xRow3 += 1;

            if (hGates[0].cols[i].activeSelf)
                hRow1 += 1;
            if (hGates[1].cols[i].activeSelf)
                hRow2 += 1;
            if (hGates[2].cols[i].activeSelf)
                hRow3 += 1;
        }

        if (hRow1 + hRow2 + hRow3 == 0)
        {
            finalResult = (xRow1 % 2) * 4 + (xRow2 % 2) * 2 + xRow3 % 2;
            for (int i = 0; i < 8; i++)
            {
                finalPaddles[i].SetActive(false);
                prePaddles[i].SetActive(false);
            }

            EnableFinalPaddle(finalResult);
        }

        else
        {
            if (ballTransform.position.x > 3.5)
            {
                List<int> activePrePaddles = new List<int>();

                // Find the indices of active prePaddles
                for (int i = 0; i < prePaddles.Length; i++)
                {
                    if (prePaddles[i].activeSelf)
                    {
                        activePrePaddles.Add(i);
                    }
                }

                if (activePrePaddles.Count > 0)
                {
                    int randIndex = Random.Range(0, activePrePaddles.Count);
                    int selectedPaddle = activePrePaddles[randIndex];

                    for (int i = 0; i < 8; i++)
                    {
                        finalPaddles[i].SetActive(false);
                        prePaddles[i].SetActive(false);
                    }

                    EnableFinalPaddle(selectedPaddle);
                }

                return;
            }

            for (int i = 0; i < 8; i++)
            {
                finalPaddles[i].SetActive(false);
                prePaddles[i].SetActive(false);
            }

            if (hRow1 > 0)
            {
                EnablePrePaddle(4 + (xRow2 % 2) * 2 + xRow3 % 2);
                EnablePrePaddle((xRow2 % 2) * 2 + xRow3 % 2);

                if (hRow2 > 0)
                {
                    EnablePrePaddle(4 + 2 + xRow3 % 2);
                    EnablePrePaddle(4 + xRow3 % 2);

                    EnablePrePaddle(2 + xRow3 % 2);
                    EnablePrePaddle(xRow3 % 2);

                    if (hRow3 > 0)
                    {
                        EnablePrePaddle(4 + 2 + 1);
                        EnablePrePaddle(4 + 2);

                        EnablePrePaddle(4 + 1);
                        EnablePrePaddle(4);

                        EnablePrePaddle(2 + 1);
                        EnablePrePaddle(2);

                        EnablePrePaddle(1);
                        EnablePrePaddle(0);
                    }
                }
                else if (hRow3 > 0)
                {
                    EnablePrePaddle(4 + (xRow2 % 2) * 2 + 1);
                    EnablePrePaddle(4 + (xRow2 % 2) * 2);

                    EnablePrePaddle((xRow2 % 2) * 2 + 1);
                    EnablePrePaddle((xRow2 % 2) * 2);
                }
            }

            else if (hRow2 > 0)
            {
                EnablePrePaddle((xRow1 % 2) * 4 + 2 + xRow3 % 2);
                EnablePrePaddle((xRow1 % 2) * 4 + xRow3 % 2);

                if (hRow3 > 0)
                {
                    EnablePrePaddle((xRow1 % 2) * 4 + 2 + 1);
                    EnablePrePaddle((xRow1 % 2) * 4 + 2);

                    EnablePrePaddle((xRow1 % 2) * 4 + 1);
                    EnablePrePaddle((xRow1 % 2) * 4);
                }
            }

            else 
            {
                EnablePrePaddle((xRow1 % 2) * 4 + (xRow2 % 2) * 2 + 1);
                EnablePrePaddle((xRow1 % 2) * 4 + (xRow2 % 2) * 2);
            }

        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentRow = (currentRow + 1) % 3;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentRow = (currentRow - 1) % 3;

            if (currentRow < 0)
                currentRow += 3;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentCol = (currentCol + 1) % 10;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentCol = (currentCol - 1) % 10;

            if (currentCol < 0)
                currentCol += 10;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EnableXGate(currentRow, currentCol);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnableHGate(currentRow, currentCol);
        }

        EnableSelection(currentRow, currentCol);
        UpdatePaddle();
    }
}

[System.Serializable]
public struct GameObjectStruct
{
    public GameObject[] cols;
}