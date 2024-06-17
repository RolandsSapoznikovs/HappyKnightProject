using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonSetup : MonoBehaviour
{
    public Button loadButton;

    private void Start()
    {
        if (SceneController.instance != null)
        {
            // Add listeners to buttons
            loadButton.onClick.AddListener(SceneController.instance.LoadGame);
            Debug.Log("ButtonSetup: Save and Load buttons set up");
        }
        else
        {
            Debug.LogWarning("SceneController instance is null in ButtonSetup");
        }
    }
}