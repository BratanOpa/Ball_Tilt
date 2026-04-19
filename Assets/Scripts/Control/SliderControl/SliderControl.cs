using UnityEngine;

public class SliderControl : MonoBehaviour
{
    float x;
    float y;

    public void OnEnable()
    {
        TiltControl tiltControl = FindFirstObjectByType<TiltControl>();
        if(tiltControl != null)
        {
            tiltControl.SetSliderControl(this);
        }
    }

    public void SetX(float x)
    {
        this.x = x;
    }

    public void SetY(float y)
    {
        this.y = y;
    }

    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }
}
