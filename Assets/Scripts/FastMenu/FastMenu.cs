using UnityEngine;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    [Header("Control Mode")]
    public GameObject joystick;
    // public TiltControl tiltControl;
    public TiltControl[] tiltControls; // NYTT: stöd för flera bollar
    public Button calibrateButton;
    public Slider sensitivitySlider;
    public Slider deadzoneSlider;
    public Slider musicSlider;
    public Slider volumeSlider;

    private bool joystickActive = false;

    [Header("Menus")]
    public GameObject settingsPanel;
    public GameObject fastMenu;

    public void Start()
    {
        animator = GetComponent<Animator>();

        // tiltControl = GameObject.FindGameObjectWithTag("Player")
        //                 .GetComponent<TiltControl>();

        tiltControls = FindObjectsByType<TiltControl>(FindObjectsSortMode.None); //  hämta alla bollar
        Debug.Log("TiltControls found: " + tiltControls.Length);

        joystickActive = GameSettings.useJoystick;

        joystick.SetActive(joystickActive);

        // tiltControl.useTilt = !joystickActive;
        // tiltControl.useJoystick = joystickActive;

        foreach (var tc in tiltControls) //  applicera på alla
        {
            tc.useTilt = !joystickActive;
            tc.useJoystick = joystickActive;
        }

        calibrateButton.interactable = !joystickActive;

        settingsPanel.SetActive(false);

        sensitivitySlider.value = GameSettings.sensitivity;
        deadzoneSlider.value = GameSettings.deadZone;
        musicSlider.value = GameSettings.musicVolume;
        volumeSlider.value = GameSettings.sfxVolume;
    }

    public void toggleMenu()
    {
        animator.SetTrigger("Toggle");
    }

    public void toggleControl()
    {
        joystickActive = !joystickActive;
        GameSettings.useJoystick = joystickActive;

        joystick.SetActive(joystickActive);

        // tiltControl.useTilt = !joystickActive;
        // tiltControl.useJoystick = joystickActive;

        foreach (var tc in tiltControls) // applicera på alla
        {
            tc.useTilt = !joystickActive;
            tc.useJoystick = joystickActive;
        }

        calibrateButton.interactable = !joystickActive;

        Debug.Log("Joystick Active: " + joystickActive);
    }

    public void Calibrate()
    {
        // var tiltControl = GameObject.FindGameObjectWithTag("Player")
        //                            .GetComponent<TiltControl>();

        foreach (var tc in tiltControls) // kalibrera alla
        {
            tc.Calibrate();
            Debug.Log("Calibration triggered on: " + tc.name);
        }
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

    public void SetSensitivity(float value)
    {
        // tiltControl.SetSensitivity(value);

        foreach (var tc in tiltControls) // ändra alla
        {
            tc.SetSensitivity(value);
        }
    }

    public void SetDeadZone(float value)
    {
        // tiltControl.SetDeadZone(value);

        foreach (var tc in tiltControls) // ändra alla
        {
            tc.SetDeadZone(value);
        }
    }
}