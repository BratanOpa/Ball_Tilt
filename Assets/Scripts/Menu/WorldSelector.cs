using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using static System.TimeZoneInfo;
using UnityEditor.Rendering.LookDev;

// Hanterar world selection-menyn
public class WorldSelector : MonoBehaviour
{
    [SerializeField] GameObject[] worlds; // Alla world panels
    [SerializeField] AudioClip clickSound;

    private int currentWorld = 0;

    [SerializeField] private Button[] worldButtons;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private TextMeshProUGUI completionText;
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private float slideTime = 0.5f;
    [SerializeField] private float slideDistance = 1920f;
    private bool isSliding=false;

    void Start()
    {
        ShowWorld(currentWorld);
    }

    // Byter till nästa world
    public void NextWorld()
    {
        if (isSliding) 
            return;

        int nextWorld = currentWorld + 1;

        // Börja om från början om vi passerar sista
        if (nextWorld >= worlds.Length)
            nextWorld = 0;

        StartCoroutine(slideWorlds(currentWorld, nextWorld, true)); //true/right

        AudioManager.Instance.PlaySFX(clickSound);
    }

    // Byter till föregående world
    public void PreviousWorld()
    {
        if (isSliding)
            return;

        int prevWorld = currentWorld - 1;

        // Hoppa till sista världen om vi går under 0
        if (prevWorld < 0)
            prevWorld = worlds.Length - 1;

        StartCoroutine(slideWorlds(currentWorld, prevWorld, false)); //false/left

        AudioManager.Instance.PlaySFX(clickSound);
    }

    private IEnumerator slideWorlds(int fromIndex, int toIndex, bool direction) // direction true/right & false/left
    {
        isSliding = true;
        // inte samma prev och next som i förra, här är "prev" den nuvarande
        // och next blir den som man byter till (oavsett riktning)
        GameObject prevWorld = worlds[fromIndex];
        GameObject nextWorld = worlds[toIndex];
        RectTransform prevRect = prevWorld.GetComponent<RectTransform>();
        RectTransform nextRect = nextWorld.GetComponent<RectTransform>();

        nextWorld.SetActive(true); // visar nya världen

        float dirValue = direction ? 1f : -1f;

        Vector2 prevStart = Vector2.zero;
        // hade problem med canvas directioner, vet inte vad albin gjorde med dem
        Vector2 prevEnd = Vector2.up * slideDistance * dirValue;
        Vector2 nextStart = Vector2.down * slideDistance * dirValue;
        Vector2 nextEnd = Vector2.zero;

        nextRect.anchoredPosition = nextStart;
        float elapsed = 0f;

        while (elapsed < slideTime)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / slideTime;

            t = Mathf.SmoothStep(0, 1, t);

            prevRect.anchoredPosition = Vector2.Lerp(prevStart, prevEnd, t);
            nextRect.anchoredPosition = Vector2.Lerp(nextStart, nextEnd, t);

            yield return null;
        }

        // frys rörelsen
        prevRect.anchoredPosition = Vector2.zero;
        nextRect.anchoredPosition = Vector2.zero;
        
        // göm gammal vis a ny
        prevWorld.SetActive(false);
        currentWorld = toIndex;
        ShowWorld(currentWorld);

        isSliding = false;
    }

    // Visar vald world och uppdaterar UI
    void ShowWorld(int index)
    {
        // Visa bara vald world
        for (int i = 0; i < worlds.Length; i++)
        {
            worlds[i].SetActive(i == index);
        }

        int worldIndex = index + 1;

        // Kollar om världen är upplåst
        bool unlocked = SaveManager.IsWorldUnlocked(worldIndex);

        // Aktivera/inaktivera knapp
        worldButtons[index].interactable = unlocked;

        // Visa låsikon om världen är låst
        lockImage.SetActive(!unlocked);

        // Visa completion % för världen
        int percent = SaveManager.getWorldCompletionPercentage(worldIndex);

        completionText.text = percent + "%";

        int collected = SaveManager.GetWorldCollectedCoins(worldIndex);

        int total = SaveManager.GetWorldTotalCoins(worldIndex);

        coinsText.text = "Coins: " + collected + " / " + total;


    }

    // Uppdaterar världen som visas just nu
    public void RefreshCurrentWorld()
    {
        ShowWorld(currentWorld);
    }

    public void DebugUnlockAll()
    {
        SaveManager.UnlockAllLevels();

        RefreshCurrentWorld();
    }
}