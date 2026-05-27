using UnityEngine;

public enum ControlMode
{
    Tilt,
    Joystick,
    Slider
}

public enum JoystickMode
{
    Left,
    Right,
    Touch
}


public static class GameSettings
{
    public static float sensitivity = 0f;
    public static float deadZone = 0.05f;
    public static float musicVolume = 0.5f;
    public static float sfxVolume = 3f;
    public static bool musicMuted = false;

    public static ControlMode controlMode = ControlMode.Tilt; 
    public static JoystickMode joystickMode = JoystickMode.Left;

    public static Vector3 calibrationOffset;

    public static bool freezeScreenActive = false;

    public static void ResetToDefaults()
    {
        sensitivity = 1.3f;
        deadZone = 0.05f;
        musicVolume = 0.5f;
        sfxVolume = 3f;
        musicMuted = false;

        controlMode = ControlMode.Tilt;
        joystickMode = JoystickMode.Left;

        calibrationOffset = Vector3.zero;
    }
}