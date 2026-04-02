using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltLight : MonoBehaviour
{
    public Vector3 offset;
    public float speed;
    public float amplitude;

    void Start()
    {

    }

    void Update()
    {
        Vector3 tilt = Input.acceleration.normalized;
        Quaternion target = Quaternion.Euler(new Vector3(tilt.x, tilt.y, tilt.z) * amplitude + offset);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
    }
}
