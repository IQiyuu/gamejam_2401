
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    public float speed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpHeight = 10f;
    public int maxJumps = 2;
    int jumpsRemaing;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f,0.05f);
    public LayerMask groundLayer;




    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocityY);
        GroundCheck();
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaing = maxJumps;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(jumpsRemaing>0)
        {
            if(context.performed)
            {
                //Holding jump button  = max height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpHeight);
                jumpsRemaing --;
            }
            else if(context.canceled)
            {
                //Tapping jump button = mid height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY*0.5f);
                jumpsRemaing--;
            }
        
        }
         
    }

    //Gizmos aren't visible on the game execution (dev tool).
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
