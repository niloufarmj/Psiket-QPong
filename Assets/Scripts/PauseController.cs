using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public void Restart()
    {
        GameManager.instance.ResumeGame();
        GameManager.instance.reset = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
