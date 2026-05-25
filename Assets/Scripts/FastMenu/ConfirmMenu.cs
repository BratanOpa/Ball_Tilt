using UnityEngine;

public class ConfirmMenu : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confirmPanel.SetActive(false);
    }

    public void ShowConfirm()
    {
        confirmPanel.SetActive(true);
    }
    public void HideConfirm()
    {
        confirmPanel.SetActive(false);
    }

}
