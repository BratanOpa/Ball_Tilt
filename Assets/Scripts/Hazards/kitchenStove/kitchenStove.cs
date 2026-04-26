using UnityEngine;

public class kitchenStove : MonoBehaviour
{
    private float currentTimer;

    [Header("Timer settings")]
    [SerializeField] private float timerDuration;
    [SerializeField] private float startDelay;

    [Header("Materials")]
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;
    private Renderer stoveRenderer;


    void Start()
    {
        currentTimer = timerDuration;
        stoveRenderer = GetComponent<Renderer>();
    }


    void Update()
    {
        if (startDelay > 0)
        {
            startDelay -= Time.deltaTime;
            return;
        }

        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            changeState();
            resetTimer();
        }
    }
    private void resetTimer()
    {
        currentTimer = timerDuration;
    }
    private void changeState()
    {
        if (gameObject.CompareTag("stoveOn"))
        {
            gameObject.tag = "stoveOff";
            stoveRenderer.material = offMaterial;
        }
        else if (gameObject.CompareTag("stoveOff"))
        {
            gameObject.tag = "stoveOn";
            stoveRenderer.material = onMaterial;
        }
        else
        {
            gameObject.tag = "stoveOff";
            stoveRenderer.material = offMaterial;
        }
    }
}
