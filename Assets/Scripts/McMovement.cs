using System.Collections;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    public static bool canMove = true;


    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
{
    if (!canMove) 
    {
        rb.linearVelocity = Vector2.zero; // Stop movement instantly
        return;
    }

    if (isDashing) return;

    float moveX = Input.GetAxisRaw("Horizontal");
    float moveY = Input.GetAxisRaw("Vertical");
    moveInput = new Vector2(moveX, moveY).normalized;


    if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && moveInput != Vector2.zero)
    {
        StartCoroutine(Dash());
    }
}


    void FixedUpdate()
{
    if (!canMove)
    {
        rb.linearVelocity = Vector2.zero;
        return;
    }

    if (!isDashing)
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}


    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocity = moveInput * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
