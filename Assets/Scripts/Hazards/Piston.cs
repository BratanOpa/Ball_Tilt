using UnityEngine;
using System.Collections;

public class Piston : MonoBehaviour
{
    [Header("Movement")]
    public Transform pistonVisual;   // Det som rör sig fram och tillbaka
    public Vector3 pushDirection = Vector3.forward;
    public float pushDistance = 0.5f;
    public float pushSpeed = 20f;
    public float returnSpeed = 5f;

    [Header("Force")]
    public float force = 20f;

    private Vector3 startLocalPos;
    private bool isMoving = false;

    void Start()
    {
        startLocalPos = pistonVisual.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMoving) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            // Applicera kraft på bollen i pistonens riktning
            rb.AddForce(pushDirection.normalized * force, ForceMode.Impulse);

            // Starta animation
            StartCoroutine(PushRoutine());
        }
    }

    IEnumerator PushRoutine()
    {
        isMoving = true;

        Vector3 targetPos = startLocalPos + pushDirection.normalized * pushDistance;

        // Gå fram snabbt
        while (Vector3.Distance(pistonVisual.localPosition, targetPos) > 0.01f)
        {
            pistonVisual.localPosition = Vector3.MoveTowards(
                pistonVisual.localPosition,
                targetPos,
                pushSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Vänta lite (valfritt)
        yield return new WaitForSeconds(0.05f);

        // Gå tillbaka långsammare
        while (Vector3.Distance(pistonVisual.localPosition, startLocalPos) > 0.01f)
        {
            pistonVisual.localPosition = Vector3.MoveTowards(
                pistonVisual.localPosition,
                startLocalPos,
                returnSpeed * Time.deltaTime
            );
            yield return null;
        }

        isMoving = false;
    }
}