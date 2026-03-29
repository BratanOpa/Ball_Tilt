using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 tilt = Input.acceleration;
        print(tilt);
        rb.AddForce(new Vector3(tilt.x, tilt.z, tilt.y)*speed);
    }

    void Update()
    {
        
    }
}
