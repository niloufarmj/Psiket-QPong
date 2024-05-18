using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    [SerializeField] public Button[] selectedButtons;
    int currentSelected = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelected++;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSelected--;
        }

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
