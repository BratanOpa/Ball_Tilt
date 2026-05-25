using TMPro;
using UnityEngine;

public class LevelIntroText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI introText;

    private void Start()
    {
        endLevelTrigger trigger = FindFirstObjectByType<endLevelTrigger>();

        if (trigger == null)
            return;

        int currentWorld = trigger.GetCurrentWorld();
        int currentLevel = trigger.GetCurrentLevel();

        string worldName = SaveManager.worldNames[currentWorld - 1];

        int totalLevels = SaveManager.worldLevelCounts[currentWorld - 1];

        introText.text = worldName + ": Level " + currentLevel + "/" + totalLevels;
    }
}
