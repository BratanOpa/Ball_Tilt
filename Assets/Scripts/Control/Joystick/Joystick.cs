using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, JoystickInterface, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    private Vector2 inputVector;
    private float limitHandle = 1.5f;  //Limits ho far from background handle can move


    public void OnEnable()
    {
        var tiltControls = FindObjectsByType<TiltControl>(FindObjectsSortMode.None);
        foreach (var tc in tiltControls)
        {
            if (tc != null)
            {
                tc.SetJoystick(this);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, eventData.position, eventData.pressEventCamera, out pos);

        pos.x = pos.x / background.sizeDelta.x;
        pos.y = pos.y / background.sizeDelta.y;

        inputVector = new Vector2(pos.x * limitHandle, pos.y * limitHandle);
        inputVector = (inputVector.magnitude > 1) ? inputVector.normalized : inputVector;

        handle.anchoredPosition = new Vector2(
            inputVector.x * (background.sizeDelta.x / limitHandle),
            inputVector.y * (background.sizeDelta.y / limitHandle));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal() => inputVector.x;
    public float Vertical() => inputVector.y;
    public Vector2 getPosition()
    {
        return inputVector;
    }

}
