using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private Transform teleportPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.MovePosition(teleportPosition.position);
            }
        }
    }
}
