using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltCamera : MonoBehaviour
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
        print(tilt);
        Quaternion target = Quaternion.Euler(new Vector3(tilt.y, tilt.x, 0) * amplitude + offset);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
    }
}
