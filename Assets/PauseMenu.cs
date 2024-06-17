using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public GameObject SettingsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SaveGame()
    {
        Debug.Log("Saving Game");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Settings()
    {
        SettingsMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
    }

    public void ChangeVolume (float Volume)
    {
        audioMixer.SetFloat("Volume", Volume);
    }

    public void ChangeGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ChangeFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
