using System;
using UnityEngine;

public class catAttack : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    public Action OnPawDestroyed; // triggern prenumererar på detta

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Förstör BARA vid träff med spelare eller mark — inte skuggan!
        if (!other.CompareTag("Player") && !other.CompareTag("Ground")) return;

        DestroyPaw();
    }

    private void DestroyPaw()
    {
        OnPawDestroyed?.Invoke(); // meddela triggern innan Destroy
        Destroy(gameObject);
    }
}