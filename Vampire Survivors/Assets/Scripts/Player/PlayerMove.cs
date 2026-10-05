using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    Vector2 moveInput;

    [SerializeField] float moveSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = (moveInput * moveSpeed);

        //changes direction of sprite based on movement
        if (rb.linearVelocityX < 0) 
        {
            sr.flipX = true; 
        }
        else if (rb.linearVelocityX > 0) 
        { 
            sr.flipX = false; 
        }

        //stops animator when not moving
        if (rb.linearVelocity != new Vector2(0, 0))
        {
            anim.speed = 1f;
        }
        else anim.speed = 0f;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
