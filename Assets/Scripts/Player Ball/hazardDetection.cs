using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class hazardDetection : MonoBehaviour
{
    [SerializeField] private TransitionManager transition;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private String[] hazardTags = {"Hole Hazard" };

    private Rigidbody rb;

    private bool isRespawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isRespawning) return;
        foreach(String tag in hazardTags)
        {
            if (other.CompareTag(tag))
            {
                isRespawning = true;
                StartCoroutine(Respawn());
                break;
            }
        }
    }
    
    IEnumerator Respawn()
    {
        transition.ActivateHazardHoleDeathAnimation();

        rb.isKinematic = true; // freezes the ball

        yield return new WaitForSeconds(1.4f);
        transform.position = spawnPosition.position;

        rb.isKinematic = false;

        isRespawning = false;
    }
}
