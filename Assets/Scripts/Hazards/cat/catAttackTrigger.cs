using UnityEngine;

public class catAttackTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pawPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float timer = 1f;

    private float currentTimer;
    private float exitDelayTimer = 0.2f;
    private bool runExitDelay = false;
    private bool canAttack = true;
    private catAttack activePaw = null; // direktreferens istället för FindGameObjectWithTag

    private void Start()
    {
        currentTimer = timer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        runExitDelay = false;
        exitDelayTimer = 0.2f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        runExitDelay = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0 && canAttack)
            Attack();
    }

    private void Attack()
    {
        canAttack = false;
        currentTimer = timer;

        GameObject pawObj = Instantiate(
            pawPrefab,
            spawnPoint.transform.position,
            transform.rotation
        );

        // Spara direktreferens och registrera callback för när tassen förstörs
        activePaw = pawObj.GetComponent<catAttack>();
        if (activePaw != null)
            activePaw.OnPawDestroyed += OnPawDestroyed;
    }

    private void OnPawDestroyed()
    {
        canAttack = true;
        activePaw = null;
    }

    private void Update()
    {
        if (!runExitDelay) return;

        exitDelayTimer -= Time.deltaTime;
        if (exitDelayTimer <= 0)
        {
            exitDelayTimer = 0.2f;
            currentTimer = timer;
            runExitDelay = false;
        }
    }
}