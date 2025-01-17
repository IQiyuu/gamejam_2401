
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator; 
    bool isFacingRight = true;

    [Header("Movement")]
    public float baseSpeed = 10f;
    public float currentSpeed;
    public float speedMultiplier =1.5f;
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
        
        
        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed, rb.linearVelocityY);
        GroundCheck();
        Flip();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = baseSpeed*speedMultiplier;
        }
        else
        {
            currentSpeed = baseSpeed;
        }
        animator.SetFloat("yVelocity", rb.linearVelocityY);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
        

        

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
                animator.SetTrigger("Jump");
            }
            else if(context.canceled)
            {
                //Tapping jump button = mid height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY*0.5f);
                jumpsRemaing--;
                animator.SetTrigger("Jump");
            }
        
        }
         
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement <0|| !isFacingRight && horizontalMovement >0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    



    //Gizmos aren't visible on the game execution (dev tool).
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
