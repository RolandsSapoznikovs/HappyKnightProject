using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInitializer : MonoBehaviour
{
    public Text CoinCounterText;

    private void Start()
    {
        if (SceneController.instance != null)
        {
            SceneController.instance.SetCoinCounterText(CoinCounterText);
            Debug.Log("UIInitializer started and CoinCounter set");
        }
        else
        {
            Debug.LogWarning("SceneController instance is null in UIInitializer");
        }
    }
}
