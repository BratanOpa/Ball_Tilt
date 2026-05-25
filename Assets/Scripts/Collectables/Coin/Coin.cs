using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private AudioClip pickupSound;

    private bool collected = false;

    private void Start()
    {
        CoinManager.Instance.totalCoins++;

        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play(0,-1,Random.Range(0f, 1f));
        }

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
                ParticleSystem ps = effect.GetComponent<ParticleSystem>();

                if (ps != null)
                {
                    ps.Play();
                }

                Destroy(effect, 2f);
            }

            AudioManager.Instance.PlaySFX(pickupSound, 0.4f);

            Destroy(gameObject);
        }
    }
}