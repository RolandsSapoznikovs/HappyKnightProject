using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    public void MainMenu()
   {
        SceneManager.LoadScene("Main Menu");
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
