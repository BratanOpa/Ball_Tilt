using UnityEngine;

public class BounceWall : MonoBehaviour
{
    public float bounceForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            // Calculate direction based on the trampoline's rotation
            // transform.up works whether it's on floor (up), wall (sideways), etc.
            Vector3 bounceDirection = transform.up;

           // Optionally remove existing downward velocity for a cleaner bounce
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Apply force
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            Debug.Log("Boing!");
        }
    }
}