using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Follow Settings")]
    public float smoothTime = 0.15f;
    public float catchUpSmoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    [Header("Dead Zone")]
    public Vector2 deadZoneSize = new Vector2(1.2f, 0.8f);

    [Header("Look Ahead")]
    public float lookAheadDistance = 3f;
    public float lookAheadSmoothTime = 0.12f;

    private Vector3 currentLookAhead;
    private Vector3 lookAheadVelocity;
    private Rigidbody2D targetRb;

    void Start()
    {
        if (target != null)
            targetRb = target.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = transform.position;

        // 🎯 DEAD ZONE
        Vector3 toTarget = target.position - transform.position;

        if (Mathf.Abs(toTarget.x) > deadZoneSize.x)
            desiredPosition.x = target.position.x - Mathf.Sign(toTarget.x) * deadZoneSize.x;

        if (Mathf.Abs(toTarget.y) > deadZoneSize.y)
            desiredPosition.y = target.position.y - Mathf.Sign(toTarget.y) * deadZoneSize.y;

        // 🚀 VELOCITY-BASED LOOK-AHEAD
        Vector2 velocity2D = targetRb != null ? targetRb.linearVelocity : Vector2.zero;

        Vector3 lookAheadTarget = Vector3.zero;

        if (velocity2D.magnitude > 0.1f)
        {
            lookAheadTarget = (Vector3)(velocity2D.normalized * lookAheadDistance);
        }

        currentLookAhead = Vector3.SmoothDamp(
            currentLookAhead,
            lookAheadTarget,
            ref lookAheadVelocity,
            lookAheadSmoothTime
        );

        desiredPosition += currentLookAhead;

        // 🧲 SOFT CATCH-UP
        float currentSmooth = velocity2D.magnitude < 0.1f ? catchUpSmoothTime : smoothTime;

        desiredPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            currentSmooth
        );
    }
}
