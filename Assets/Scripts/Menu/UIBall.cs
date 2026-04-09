using UnityEngine;

public class UIBall : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 800f;              // Hur snabbt tilt påverkar bollen
    [SerializeField] float maxSpeed = 1000f;          // Maxhastighet (för stabilitet)
    [SerializeField] float friction = 0.98f;          // Broms (0.98 = lite friktion)

    [Header("Bounce")]
    [SerializeField] float bounceDamping = 0.9f;      // Hur mycket fart som behålls vid studs
    [SerializeField] float minBounceForce = 200f;     // Minsta studs så den inte fastnar

    [Header("Targets (UI to bounce on)")]
    [SerializeField] Transform[] bounceTargets;       // Dra in PlayButton etc här

    private Vector2 velocity;
    private RectTransform rect;
    private RectTransform parentRect;

    void Start()
    {
        rect = GetComponent<RectTransform>();

        // Hitta canvasen som detta UI-element tillhör
        Canvas canvas = GetComponentInParent<Canvas>();
        parentRect = canvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        ApplyTiltMovement();
        Move();
        ClampToBounds();
        HandleTargetCollisions();
    }

    //  Tilt  acceleration
    void ApplyTiltMovement()
    {
        Vector3 tilt = Input.acceleration;

        // Konvertera mobilens tilt till UI-rörelse
        Vector2 input = new Vector2(tilt.x, tilt.y);

        velocity += input * speed * Time.deltaTime;

        // Begränsa maxhastighet 
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        // Friktion (så den lugnar ner sig)
        velocity *= friction;
    }

    //  Flytta bollen
    void Move()
    {
        rect.anchoredPosition += velocity * Time.deltaTime;
    }

    //  Studs mot kanter (panelens bounds)
    void ClampToBounds()
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, rect.position);

        float halfW = rect.rect.width / 2;
        float halfH = rect.rect.height / 2;

        // Höger
        if (screenPos.x > Screen.width - halfW)
        {
            screenPos.x = Screen.width - halfW;
            velocity.x = -Mathf.Abs(velocity.x) * bounceDamping;
        }

        // Vänster
        if (screenPos.x < halfW)
        {
            screenPos.x = halfW;
            velocity.x = Mathf.Abs(velocity.x) * bounceDamping;
        }

        // Topp
        if (screenPos.y > Screen.height - halfH)
        {
            screenPos.y = Screen.height - halfH;
            velocity.y = -Mathf.Abs(velocity.y) * bounceDamping;
        }

        // Botten
        if (screenPos.y < halfH)
        {
            screenPos.y = halfH;
            velocity.y = Mathf.Abs(velocity.y) * bounceDamping;
        }

        // Konvertera tillbaka till world/UI position
        rect.position = screenPos;
    }

    //  Kollision med UI-element (PlayButton etc)
    void HandleTargetCollisions()
    {
        foreach (Transform target in bounceTargets)
        {
            if (target == null) continue;

            RectTransform targetRect = target.GetComponent<RectTransform>();
            if (targetRect == null) continue;

            if (IsOverlapping(rect, targetRect))
            {
                BounceOff(targetRect);
            }
        }
    }

    //  Kolla overlap (UI-version av collision)
    bool IsOverlapping(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            b,
            RectTransformUtility.WorldToScreenPoint(null, a.position)
        );
    }

    //  Smart studs (riktning baserad på var vi träffar)
    void BounceOff(RectTransform target)
    {
        Vector2 direction = (rect.anchoredPosition - target.anchoredPosition).normalized;

        // Säkerställ att vi alltid får en studs
        if (direction.magnitude < 0.1f)
            direction = Random.insideUnitCircle.normalized;

        velocity = direction * Mathf.Max(minBounceForce, velocity.magnitude) * bounceDamping;
    }
}