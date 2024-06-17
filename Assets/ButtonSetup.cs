using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour
{
    public Button saveButton;

    private void Start()
    {
        if (SceneController.instance != null)
        {
            // Add listeners to buttons
            saveButton.onClick.AddListener(SceneController.instance.SaveGame);
            Debug.Log("ButtonSetup: Save and Load buttons set up");
        }
        else
        {
            Debug.LogWarning("SceneController instance is null in ButtonSetup");
        }
    }
}