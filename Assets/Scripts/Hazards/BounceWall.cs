using UnityEngine;

public class BounceWall : MonoBehaviour
{
    public float bounceForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            // Hämta normal från kontaktpunkten (vilket håll väggen "pekar")
            Vector3 normal = collision.contacts[0].normal;

            // Reflektera hastigheten (som en spegelstuds)
            Vector3 newVelocity = Vector3.Reflect(rb.linearVelocity, normal);

            // Sätt ny hastighet med extra kraft
            rb.linearVelocity = newVelocity * bounceForce;

            // Försök hitta tilt-scriptet och lås kontrollen tillfälligt
            TiltControl tilt = rb.GetComponent<TiltControl>();
            if (tilt != null)
            {
                tilt.LockControl(); // Stoppar spelarinput en kort stund
            }
        }
    }
}