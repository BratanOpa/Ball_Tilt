using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelMenu; 

    void Start()
    {
        // När spelet startar visas mainMenu och levelMenu göms
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void PlayPressed()
    {
        // När spelaren trycker på Play:
        // Dölj mainMenu
        // sen Visa levelMenu
        //kanske nån animation och ljud här??

        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
}