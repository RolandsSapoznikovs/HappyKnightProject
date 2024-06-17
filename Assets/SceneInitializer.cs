using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitializer : MonoBehaviour
{
    private void Start()
    {
        SceneController sceneController = SceneController.instance;
        Text CoinCounter = GameObject.Find("CoinCounter").GetComponent<Text>();
        if (sceneController != null && CoinCounter != null)
        {
            sceneController.SetCoinCounterText(CoinCounter);
        }
    }
}
