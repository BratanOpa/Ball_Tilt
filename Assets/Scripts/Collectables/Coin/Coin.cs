using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private AudioClip pickupSound;

    private bool collected = false;

    private void Start()
    {
        CoinManager.Instance.totalCoins++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            CoinManager.Instance.CollectCoin();

            // Spawn particles
            if (pickupEffect != null)
            {
                GameObject effect = Instantiate(
                    pickupEffect,
                    transform.position,
                    Quaternion.identity
                );

                Destroy(effect, 2f);
            }

            AudioManager.Instance.PlaySFX(pickupSound, 0.4f);

            Destroy(gameObject);
        }
    }
}