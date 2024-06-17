using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    private SceneController sceneController;

    private void Start()
    {
        sceneController = SceneController.instance;
    }

    public void AddCoins(int amount)
    {
        if (sceneController != null)
        {
            sceneController.AddCoins(amount);
        }
    }
}
