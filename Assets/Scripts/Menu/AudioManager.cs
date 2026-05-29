using UnityEngine;
using UnityEngine.SceneManagement;


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
        DontDestroyOnLoad(gameObject); // �verlever scenbyten

        SceneManager.sceneLoaded += OnSceneLoaded; // f�rbereder att k�ra onSceneLoaded varje g�ng en scen laddas. Unity k�r OnSceneLoaded(scene, mode);

        // Skapa AudioSources om de inte finns
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;

    }


    //k�r innan n�gon scen laddas, s� att ljudet �r redo direkt
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        GameObject obj = new GameObject("AudioManager");
        obj.AddComponent<AudioManager>();
        
    }


    private void Start()
    {
        musicSource.volume = GameSettings.musicVolume;
        sfxSource.volume = GameSettings.sfxVolume;
        musicSource.mute = GameSettings.musicMuted;

        AudioClip clip = Resources.Load<AudioClip>("Audio/My Song 13");
        PlayMusic(clip);

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("New scene loaded: " + scene.name);

        // Musiken m�ste lika i resources/Audio och ha samma namn som scenen nedan f�r att spelas automatiskt, annars spelas defaultmusiken

        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(Resources.Load<AudioClip>("Audio/My Song 13"));
                break;

            case "Wood1":
                PlayMusic(Resources.Load<AudioClip>("Audio/My Song 13"));
                break;

            case "dsvLevel1":
                PlayMusic(Resources.Load<AudioClip>("Audio/My Song 13"));
                break;
            case "GolfLevel1":
                PlayMusic(Resources.Load<AudioClip>("Audio/My Song 13"));
                break;
            default:
                PlayMusic(Resources.Load<AudioClip>("Audio/My Song 13"));
                break;
        }

    }


    //f�r att spela ny l�t vid ny v�rld exempelvis
    public void PlayMusic(AudioClip newClip)
    {
        // Om samma l�t redan spelas -> g�r inget
        if (currentTrack == newClip)
            return;

        currentTrack = newClip;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    //kalla p� denna i andra skript f�r att spela ljudeffekter. AudioManager.Instance.PlaySFX(soundeffectName, 0.4f);
    public void PlaySFX(AudioClip clip, float volume = 1f) //volymen kan justeras per ljud, t.ex. f�r att g�ra vissa ljudeffekter mer diskreta. generellt anv�nds fortfarande sliderns volym
    {
        sfxSource.PlayOneShot(clip, volume);
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
    public void toggleSFX(bool muted)
    {
        sfxSource.mute = muted;
        Debug.Log("sfx is muted: " + muted);
    }
    public bool getSFXMuted()
    {
       return sfxSource.mute;
    }

    public void ToggleMusic(bool muted)
    {
        musicSource.mute = muted;
        GameSettings.musicMuted = muted;
    }
}