using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    private void EnableSelection()
    {
        for (int s = 0; s < selections.Length; s++)
        {
            for (int k = 0; k < 10; k++)
            {
                selections[s].cols[k].SetActive(false);
            }
        }
        selections[currentRow].cols[currentCol].SetActive(true);
    }

    public void EnableHGate()
    {
        if (GameManager.instance.paused)
            return;

        xGates[currentRow].cols[currentCol].SetActive(false);
        hGates[currentRow].cols[currentCol].SetActive(!hGates[currentRow].cols[currentCol].activeSelf);
    }

    public void EnableXGate()
    {
        if (GameManager.instance.paused)
            return;

        hGates[currentRow].cols[currentCol].SetActive(false);
        xGates[currentRow].cols[currentCol].SetActive(!xGates[currentRow].cols[currentCol].activeSelf);
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
        int[] xRows = new int[selections.Length];
        int[] hRows = new int[selections.Length];
        int finalResult = 0;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < selections.Length; j++)
            {
                if (xGates[j].cols[i].activeSelf)
                    xRows[j] += 1;
                if (hGates[j].cols[i].activeSelf)
                    hRows[j] += 1;
            }
        }

        if (hRows.Sum() == 0)
        {
            for (int i = 0; i < selections.Length; i++)
            {
                finalResult += (xRows[i] % 2) * (int)(Mathf.Pow(2, selections.Length - 1 - i));
            }

            for (int i = 0; i < finalPaddles.Length; i++)
            {
                finalPaddles[i].SetActive(false);
                prePaddles[i].SetActive(false);
            }

            EnableFinalPaddle(finalResult);
        }

        else
        {
            if (ballTransform.position.x > 6)
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

                    for (int i = 0; i < finalPaddles.Length; i++)
                    {
                        finalPaddles[i].SetActive(false);
                        prePaddles[i].SetActive(false);
                    }

                    EnableFinalPaddle(selectedPaddle);
                }

                return;
            }

            for (int i = 0; i < finalPaddles.Length; i++)
            {
                finalPaddles[i].SetActive(false);
                prePaddles[i].SetActive(false);
            }

            int[] bits = new int[selections.Length];
            for (int i = 0; i < selections.Length; i++)
            {
                if (hRows[i] > 0)
                {
                    bits[i] = 2;
                }
                else
                {
                    bits[i] = xRows[i] % 2;
                }
            }

            CalculatePrePaddles(bits);

        }
        
    }

    public void CalculatePrePaddles(int[] bits)
    {
        int result = 0;
        for (int i = 0; i < bits.Length; i++)
        {
            if (bits[i] == 2)
            {
                int[] newBits0 = new int[bits.Length];
                int[] newBits1 = new int[bits.Length];

                for (int j = 0; j < bits.Length; j++)
                {
                    if (i == j)
                    {
                        newBits0[j] = 0;
                        newBits1[j] = 1;
                    }
                    else
                    {
                        newBits0[j] = bits[j];
                        newBits1[j] = bits[j];
                    }
                }
                CalculatePrePaddles(newBits0);
                CalculatePrePaddles(newBits1);
                return;
            }

            result += bits[i] * (int)(Mathf.Pow(2, bits.Length - 1 - i));
        }

        EnablePrePaddle(result);
    }

    public void MoveRight()
    {
        if (GameManager.instance.paused)
            return;

        currentCol = (currentCol + 1) % 10;
    }

    public void MoveLeft()
    {
        if (GameManager.instance.paused)
            return;

        currentCol = (currentCol - 1) % 10;

        if (currentCol < 0)
            currentCol += 10;
    }

    public void MoveUp()
    {
        if (GameManager.instance.paused)
            return;

        currentRow = (currentRow - 1) % selections.Length;

        if (currentRow < 0)
            currentRow += selections.Length;
    }

    public void MoveDown()
    {
        if (GameManager.instance.paused)
            return;

        currentRow = (currentRow + 1) % selections.Length;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            EnableXGate();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnableHGate();
        }

        EnableSelection();
        UpdatePaddle();
    }
}

[System.Serializable]
public struct GameObjectStruct
{
    public GameObject[] cols;
}