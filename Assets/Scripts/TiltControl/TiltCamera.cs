using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltCamera : MonoBehaviour
{
    public float speed = 5;
    public float amplitude = 10;
    public TiltControl control;
    private Quaternion offset;
    
    void Start()
    {
        offset = gameObject.transform.rotation;
    }

    void Update()
    {
        print("Control" + control.getControl());
        print(Input.acceleration.normalized);
        Vector3 tilt = new Vector3(-control.getControl().z, control.getControl().x, control.getControl().y);//Input.acceleration.normalized;
        Quaternion yaw   = Quaternion.AngleAxis(tilt.y * -amplitude, transform.right);   // I dont know how it works but it does, dont touch!
        Quaternion pitch = Quaternion.AngleAxis(-tilt.x * -amplitude, Vector3.right);
        Quaternion target = yaw * pitch * offset;

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
    }

}
