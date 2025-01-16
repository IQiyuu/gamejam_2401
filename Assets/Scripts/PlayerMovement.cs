
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    float direction = 0f;
    private float Direction;
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

    [Header("Runes")]
    private bool IsHolding = false;
    private float pressDuration;
    private float pressTime;
    private bool IsRolling = false;

    public bool speedRune = false;
    public bool jumpRune = false;
    public bool levitationRune = false;

    public bool RouladeRune = false;
    public bool ChargeRune = false;


    private float speedRuneMultiplier = 1f;
    private float jumpRuneMultiplier = 1f;
    private float levitationRuneValue = 3f;

    public float Rollduration = 2f;
    public float RollSpeed = 10f;
    public float addSpeed = 1.5f;
    public float addJump = 1.5f;
    public float levitationValue = 1.5f;


    // Update is called once per frame
    void Update()
    {
        if (IsHolding)
            horizontalMovement = 0;
        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed * speedRuneMultiplier, rb.linearVelocityY);
        GroundCheck();
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = baseSpeed*speedMultiplier;
        else
            currentSpeed = baseSpeed;
        Direction = Input.GetAxis("Horizontal");
       // Debug.Log(Direction);
        if (Input.GetKeyDown(KeyCode.C) && GroundCheck() && IsRolling == false)
        {
            rb.linearVelocity = Vector2.zero;
            pressTime = Time.time;
            IsHolding = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (IsHolding)
            {
                pressDuration = Time.time - pressTime;
                //Debug.Log("Durée de la pression sur C : " + pressDuration + " secondes");
                StartCoroutine(Roulade());
                IsHolding = false;
            }
        }
        
        Rune();

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!IsHolding)
            horizontalMovement = context.ReadValue<Vector2>().x;
        else
            horizontalMovement = 0;
    }

    bool GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaing = maxJumps;
            return true;
        }
        return false;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(jumpsRemaing>0 && !IsHolding)
        {
            if(context.performed)
            {
                //Holding jump button  = max height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX , jumpHeight * jumpRuneMultiplier);
                jumpsRemaing --;
            }
            else if(context.canceled)
            {
                //Tapping jump button = mid height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX , rb.linearVelocityY * 0.5f * jumpRuneMultiplier);
                jumpsRemaing--;
            }
        
        }
         
    }


    private IEnumerator Roulade()
    {
        if (Direction > 0)
            direction  = 1;
        else if (Direction < 0)
            direction = -1;
       // float direction = transform.localScale.x > 0 ? 1 : -1;
        float duration = 0f;
        if (pressDuration > 1.5)
            pressDuration = 1.5f;
        if (pressDuration < 0.5f)
            pressDuration = 0.5f;
        while (duration < pressDuration * Rollduration)
        {
            duration += Time.deltaTime;
            rb.linearVelocity = new Vector2(RollSpeed * direction * speedRuneMultiplier * (pressDuration - (duration / Rollduration)) , rb.linearVelocityY);
            IsRolling = true;
            yield return null;
        }
        IsRolling = false;
    }

    void Rune()
    {
        if (speedRune)
            speedRuneMultiplier = addSpeed;
        else
            speedRuneMultiplier = 1f;
        if (jumpRune)
            jumpRuneMultiplier = addJump;
        else
            jumpRuneMultiplier = 1f;
        if (levitationRune)
            rb.gravityScale = levitationValue;
        else
            rb.gravityScale = 3f;
    }

    //Gizmos aren't visible on the game execution (dev tool).
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
