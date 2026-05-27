using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickTouch : MonoBehaviour, JoystickInterface, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    private Vector2 inputVector;
    private Image[] images;
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
        images = GetComponentsInChildren<Image>();
        DisableImages();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = eventData.position;
        EnableImages();
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
        DisableImages();
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    private void DisableImages()
    {
        foreach (Image image in images)
        {
            image.enabled = false;
        }
        GetComponent<Image>().enabled = true; //Does not allow disable self trigger
    }

    private void EnableImages()
    {
        foreach (Image image in images)
        {
            image.enabled = true;
        }
    }

    public float Horizontal() => inputVector.x;
    public float Vertical() => inputVector.y;
    public Vector2 getPosition()
    {
        return inputVector;
    }

}
