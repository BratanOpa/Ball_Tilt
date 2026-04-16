using UnityEngine;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    [Header("Control Mode")]
    public GameObject joystick;
    public TiltControl tiltControl;
    public Button calibrateButton;
    public Slider sensitivitySlider;
    public Slider deadzoneSlider;


    private bool joystickActive = false;

    [Header("Menus")]
    public GameObject settingsPanel;
    public GameObject fastMenu;



    public void Start()
    {
        animator = GetComponent<Animator>();

       
        calibrateButton.interactable = true;
        settingsPanel.SetActive(false);
        sensitivitySlider.value = GameSettings.sensitivity;
        deadzoneSlider.value = GameSettings.deadZone;

    }

    public void toggleMenu()
    {
        animator.SetTrigger("Toggle");
    }

    public void toggleControl()
    {
        joystick.SetActive(joystickActive);

        tiltControl.useTilt = !joystickActive;
        tiltControl.useJoystick = joystickActive;

        calibrateButton.interactable = !joystickActive;


    }


    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        if (fastMenu != null)
            fastMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);

        if (fastMenu != null)
            fastMenu.SetActive(true);
    }

}
