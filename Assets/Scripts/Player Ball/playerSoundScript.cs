using System.Collections;
using UnityEngine;

public class playerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip ballCollisionSFX, playerDeathSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource rollingSource;
    private Rigidbody rb;
    private float playerVelocity;
    private float dynamicVolume;

    private bool isRespawning = false;

    //avg�r om detta �r huvudspelaren (fullt ljud + death-ljud)
    public bool isMainPlayer = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerVelocity = rb.angularVelocity.magnitude;
        dynamicVolume = Mathf.Clamp01(playerVelocity / 12f);

        //enemy f�r l�gre volym
        float volumeMultiplier = isMainPlayer ? 4f : 1.5f;     //2f : 0.6f

        
        rollingSource.volume = dynamicVolume * GameSettings.sfxVolume * volumeMultiplier;
        
        // Stoppa rolling completely under win screen
        if (GameSettings.freezeScreenActive)
        {
            rollingSource.volume = 0f;

            if (rollingSource.isPlaying)
            {
                rollingSource.Stop();
            }

            return;
        }
        else
        {
            if (!rollingSource.isPlaying && !GameSettings.freezeScreenActive)
            {
                rollingSource.Play();
            }
            return;
        }
    }
    private float GetDynamicVolume()
    {
        return dynamicVolume;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                // enemy f�r l�gre volym
                float volumeMultiplier = isMainPlayer ? 0.6f : 0.4f;
                audioSource.PlayOneShot(ballCollisionSFX, Mathf.Clamp01(playerVelocity / 30f) * GameSettings.sfxVolume * volumeMultiplier);
            }

            //bollkollision = deathljud
            if (collision.gameObject.GetComponent<hazardDetection>() != null && !isRespawning)
            {
                // endast huvudspelaren spelar death-ljud
                if (isMainPlayer)
                {
                    audioSource.PlayOneShot(playerDeathSFX, 0.6f * GameSettings.sfxVolume);
                }

                isRespawning = true;
                StartCoroutine(RespawnCooldown());
            }


        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (audioSource != null)
        {
            if (collision.gameObject.CompareTag("Hole Hazard") && !isRespawning)
            {


                audioSource.PlayOneShot(playerDeathSFX, 0.6f * GameSettings.sfxVolume);

                isRespawning = true;
                StartCoroutine(RespawnCooldown());
            }
        }
    }

    IEnumerator RespawnCooldown()
    {
        yield return new WaitForSeconds(1f);
        isRespawning = false;
    }
}