using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndSave : MonoBehaviour
{
    private const string SceneKey = "SavedScene";
    private const string CoinsKey = "TotalCoins";
    public static LoadAndSave instance;

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

    public void SaveCurrentScene(int coinCount)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(SceneKey, currentSceneName);
        PlayerPrefs.SetInt(CoinsKey, coinCount);
        PlayerPrefs.Save();
        Debug.Log($"Scene saved: {currentSceneName} | Coins saved: {coinCount}");
    }

    public IEnumerator LoadSavedSceneCoroutine()
    {
        if (PlayerPrefs.HasKey(SceneKey))
        {
            string savedSceneName = PlayerPrefs.GetString(SceneKey);
            Debug.Log($"Loading saved scene: {savedSceneName}");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(savedSceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            int coinCount = PlayerPrefs.GetInt(CoinsKey, 0);
            SceneController.instance.totalCoins = coinCount;
            SceneController.instance.UpdateCoinUI();
            Debug.Log($"Scene loaded: {savedSceneName} | Coins loaded: {coinCount}");
        }
        else
        {
            Debug.LogWarning("No scene saved.");
        }
    }
}