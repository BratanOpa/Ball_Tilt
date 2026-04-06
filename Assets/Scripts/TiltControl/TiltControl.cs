using UnityEngine;

public class TiltControl : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 offset;
    public bool isKeyboardControl;

    private Vector2 moveInput;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;  // Makes ball render in higher (120) fps while physics has 50 tps, smooth graphics
    }

    void FixedUpdate()
    {
        Vector3 tilt = Input.acceleration;
        rb.AddForce((new Vector3(tilt.y, tilt.z, -tilt.x) + offset ) * speed * rb.mass);
        if (isKeyboardControl)
        {
            keyboardControl();
        }
    }

    void keyboardControl()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += Vector3.right;
        if (Input.GetKey(KeyCode.S)) move += Vector3.left;
        if (Input.GetKey(KeyCode.A)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.D)) move += Vector3.back;

        rb.AddForce(move * speed/4);
    }

}