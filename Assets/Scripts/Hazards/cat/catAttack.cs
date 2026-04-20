using UnityEngine;

public class catAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}