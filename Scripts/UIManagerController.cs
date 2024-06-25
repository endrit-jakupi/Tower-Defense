using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerController : MonoBehaviour
{
    public Image playButton;
    public Image pauseButton;

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            pauseButton.enabled = true;
            playButton.enabled = false;
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            pauseButton.enabled = false;
            playButton.enabled = true;
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}