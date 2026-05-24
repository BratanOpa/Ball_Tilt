using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class endLevelTrigger : MonoBehaviour
{
    [SerializeField] private String nextSceneIs;
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentWorld;

    private bool finished = false;  

    private void OnTriggerEnter(Collider other)
    {
        if (finished) return;

        if (other.CompareTag("Player"))
        {
            finished = true;
            PlayerPrefs.SetString("NextLevel", nextSceneIs);

            int globalLevel = SaveManager.GetGlobalLevelIndex(currentWorld, currentLevel);


            ChangeLevel();
        }
    }
    private void ChangeLevel()
    {
        int globalLevel = SaveManager.GetGlobalLevelIndex(currentWorld, currentLevel);

        SaveManager.completeLevel(globalLevel);

        SaveManager.SaveLevelCoins(globalLevel, CoinManager.Instance.collectedCoins);

        SaveManager.SaveLevelTotalCoins(globalLevel, CoinManager.Instance.totalCoins);

        SaveManager.checkWorldUnlock(currentWorld); // Kolla om nästa värld ska låsas upp

        WinScreenUI.Instance.Show(nextSceneIs);
    }

    public string GetNextLevelName()
    {
        return nextSceneIs;
    }

}
