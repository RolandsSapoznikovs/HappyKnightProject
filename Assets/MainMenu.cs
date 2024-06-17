using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

   public AudioMixer audioMixer;

   public GameObject SettingsMenuUI;

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                SettingsMenuUI.SetActive(false);            
        }
    }

   public void PlayGame()
   {
        SceneManager.LoadSceneAsync(1);
   }

   public void QuitGame()
   {
      Application.Quit();
   }

   public void Settings()
    {
        SettingsMenuUI.SetActive(true);
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
