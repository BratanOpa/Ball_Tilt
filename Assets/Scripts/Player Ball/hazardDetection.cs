using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class hazardDetection : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole Hazard"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = spawnPosition.position;
        rb.linearVelocity = Vector3.zero;
    }
}
