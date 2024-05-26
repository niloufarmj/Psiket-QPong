using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameData data;
    public Menu[] menus;

    public GameObject singlePlayer, multiPlayer, soundOn, SoundOff, controlMenu, control, controlSelected;

    private void Start()
    {
        Initialize(data, menus, ToggleMenu);

        if (Application.isMobilePlatform)
        {
            SetPvp(false);
        }
    }

    public void SetQbits(int value)
    {
        data.Qbits = value;
    }

    public void SetAISpeed(float value)
    {
        data.aiSpeed = value;
    }

    public void SetBallSpeed(float value)
    {
        data.ballSpeed = value;
    }

    public void SetPvp(bool value)
    {
        if (Application.isMobilePlatform)
            value = false;

        data.isPvp = value;

        if (!value)
        {
            singlePlayer.SetActive(true);
            multiPlayer.SetActive(false);
        }

        else
        {
            multiPlayer.SetActive(true);
            singlePlayer.SetActive(false);
        }
       
    }

    public void SetSound(bool value)
    {
        data.soundOn = value;
        if (!value)
        {
            SoundOff.SetActive(true);
            soundOn.SetActive(false);
        }

        else
        {
            soundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
    }

    public void ControlHandler()
    {
        if (Application.isMobilePlatform)
        {
            OpenSingleGuide();
        }
        else
        {
            if (controlMenu.activeSelf)
            {
                controlMenu.SetActive(false);
                control.GetComponent<HoverController>().enabled = true;
            }
            else
            {
                controlMenu.SetActive(true);
                control.GetComponent<HoverController>().enabled = false;
                controlSelected.SetActive(true);

                for (int i = 0; i < menus.Length; i++)
                {
                    menus[i].Hide();
                }
            }
        }
    }

    public void OpenSingleGuide()
    {
        SceneManager.LoadScene(5);
    }

    public void OpenPVPGuide()
    {
        SceneManager.LoadScene(6);
    }

    public void Initialize(GameData data, Menu[] menus, Action<int> onMenuOpenerClicked)
    {
        this.menus = menus;


        menus[0].selected[0].SetActive(data.Qbits == 2);
        menus[0].selected[1].SetActive(data.Qbits == 3);

        menus[0].notSelected[0].SetActive(data.Qbits != 2);
        menus[0].notSelected[1].SetActive(data.Qbits != 3);

        menus[1].selected[0].SetActive(data.aiSpeed == 1.7f);
        menus[1].selected[1].SetActive(data.aiSpeed == 2.5f);
        menus[1].selected[2].SetActive(data.aiSpeed == 3.3f);

        menus[1].notSelected[0].SetActive(data.aiSpeed != 1.7f);
        menus[1].notSelected[1].SetActive(data.aiSpeed != 2.5f);
        menus[1].notSelected[2].SetActive(data.aiSpeed != 3.3f);

        menus[2].selected[0].SetActive(data.ballSpeed == 1.7f);
        menus[2].selected[1].SetActive(data.ballSpeed == 2.5f);
        menus[2].selected[2].SetActive(data.ballSpeed == 3.3f);

        menus[2].notSelected[0].SetActive(data.ballSpeed != 1.7f);
        menus[2].notSelected[1].SetActive(data.ballSpeed != 2.5f);
        menus[2].notSelected[2].SetActive(data.ballSpeed != 3.3f);

        soundOn.SetActive(data.soundOn);
        SoundOff.SetActive(!data.soundOn);

        singlePlayer.SetActive(!data.isPvp);
        multiPlayer.SetActive(data.isPvp);

        // Initialize menus and event handlers
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].Initialize(ToggleMenu, i);
        }
    }

    public void ToggleMenu(int index)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == index)
            {
                menus[i].ToggleVisibility();
            }
            else
            {
                menus[i].Hide();
            }
        }
    }

    public void Play()
    {
        if (Application.isMobilePlatform)
        {
            data.isPvp = false;
        }

        if (!data.isPvp && data.Qbits == 2)
        {
            SceneManager.LoadScene(1);
        }
        else if (!data.isPvp && data.Qbits == 3)
        {
            SceneManager.LoadScene(3);
        }
        else if (data.isPvp && data.Qbits == 2)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }
}


[System.Serializable]
public class Menu
{
    public Button[] menuOpener;
    public GameObject menu;
    public GameObject[] selected;
    public GameObject[] notSelected;
    public GameObject controlMenu, control, controlSelected;

    public void Initialize(Action<int> onMenuOpenerClicked, int index)
    {
        for (int i = 0; i < menuOpener.Length; i++)
        {
            menuOpener[i].onClick.AddListener(() => onMenuOpenerClicked(index));
        }

        for (int i = 0; i < selected.Length; i++)
        {
            // Add event listeners for the notSelected buttons
            int currentIndex = i;
            notSelected[i].GetComponent<Button>().onClick.AddListener(() => OnNotSelectedClicked(index, currentIndex));

        }
    }

    public void OnNotSelectedClicked(int menuIndex, int itemIndex)
    {
        // Hide all selected items of the current menu
        for (int i = 0; i < selected.Length; i++)
        {
            selected[i].SetActive(false);
        }

        // Show all notSelected items of the current menu
        for (int i = 0; i < notSelected.Length; i++)
        {
            notSelected[i].SetActive(true);
        }

        // Hide the clicked notSelected item
        notSelected[itemIndex].SetActive(false);

        // Show the corresponding selected item
        selected[itemIndex].SetActive(true);
    }

    public void Select(int index)
    {
        for (int i = 0; i < selected.Length; i++)
        {
            selected[i].SetActive(i == index);
            notSelected[i].SetActive(i != index);
        }
    }

    public void ToggleVisibility()
    {
        menuOpener[0].gameObject.SetActive(!menu.activeSelf);
        menu.SetActive(!menu.activeSelf);

        if (menu.activeSelf)
        {
            controlMenu.SetActive(false);
            control.GetComponent<HoverController>().enabled = true;
            controlSelected.SetActive(false);
        }
    }

    public void Hide()
    {
        menu.SetActive(false);
        menuOpener[0].gameObject.SetActive(false);
    }
}