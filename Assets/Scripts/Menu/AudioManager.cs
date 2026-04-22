using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip currentTrack;


    private void Awake()
    {
        // --- NY KOD ---
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // undvik duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // överlever scenbyten
    }




    private void Start()
    {
        musicSource.volume = GameSettings.musicVolume;
        sfxSource.volume = GameSettings.sfxVolume;
        musicSource.mute = GameSettings.musicMuted;



    }

    public void PlayMusic(AudioClip newClip)
    {
        // Om samma låt redan spelas -> gör inget
        if (currentTrack == newClip)
            return;

        currentTrack = newClip;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        GameSettings.musicVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
        GameSettings.sfxVolume = value;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        GameSettings.musicMuted = musicSource.mute;
    }
}