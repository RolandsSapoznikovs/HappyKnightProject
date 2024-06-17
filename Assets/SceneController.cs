using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public int totalCoins = 0;
    public Text CoinCounter;
    public LoadAndSave loadAndSave;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (loadAndSave == null)
        {
            loadAndSave = LoadAndSave.instance;
        }
        UpdateCoinUI();

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("SceneController started");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (loadAndSave == null)
        {
            loadAndSave = LoadAndSave.instance;
        }
        UpdateCoinUI();
        Debug.Log($"Scene loaded: {scene.name}");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        if (CoinCounter != null)
        {
            CoinCounter.text = "x " + totalCoins;
        }
    }

    public void SetCoinCounterText(Text newCoinCounter)
    {
        CoinCounter = newCoinCounter;
        UpdateCoinUI();
    }

    public void SaveGame()
    {
        if (loadAndSave == null)
        {
            loadAndSave = LoadAndSave.instance;
        }
        Debug.Log("Saving game...");
        loadAndSave.SaveCurrentScene(totalCoins);
    }

    public void LoadGame()
    {
        if (loadAndSave == null)
        {
            loadAndSave = LoadAndSave.instance;
        }
        Debug.Log("Loading game...");
        StartCoroutine(loadAndSave.LoadSavedSceneCoroutine());
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandlePlayerDeath()
    {
        totalCoins = Mathf.CeilToInt(totalCoins / 2.0f);
        UpdateCoinUI();
        Debug.Log($"Player died, coins halved and rounded up: {totalCoins}");
    }
}