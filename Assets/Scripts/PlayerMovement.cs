
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private ParticleSystem smokeEffect;
    public Rigidbody2D rb;
    public Animator animator; 
    bool isFacingRight = true;

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
    public LayerMask OneWayLayer;

    [Header("Runes")]
    private bool IsHolding = false;
    private float pressDuration;
    private float pressTime;
    public bool IsRolling = false;

    public bool IsCharging = false;

    public bool IsRebond = false;

    public bool[] runes = {false,false,false,false};

    public bool RouladeRune = false;
    public bool ChargeRune = false;


    private float speedRuneMultiplier = 1f;
    private float jumpRuneMultiplier = 1f;
    //private float levitationRuneValue = 3f;

    [Header("Roulade")]
    public float Rollduration = 2f;
    public float RollSpeed = 10f;
    public float addSpeed = 1.5f;
    public float addJump = 1.5f;
    public float levitationValue = 1.5f;

    public Collider2D lCol;
    public Collider2D rCol;

    float duration;

    // Update is called once per frame
    void Update()
    {
        if (IsRebond)
            return ;
        if (IsHolding)
            horizontalMovement = 0;
        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed * speedRuneMultiplier, rb.linearVelocityY);
        GroundCheck();
        Flip();
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = baseSpeed*speedMultiplier;
        else
            currentSpeed = baseSpeed;
        Direction = Input.GetAxis("Horizontal");
        if (RouladeRune) {
            if (Input.GetKeyDown(KeyCode.C) && GroundCheck() && IsRolling == false)
            {
                rb.linearVelocity = Vector2.zero;
                pressTime = Time.time;
                IsHolding = true;
            }
            animator.SetFloat("yVelocity", rb.linearVelocityY);
            animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
            

            
            if (Input.GetKeyUp(KeyCode.C))
            {
                if (IsHolding)
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
                        StartCoroutine(Roulade());
                        IsHolding = false;
                    }
                }
            }
        }
        Rune();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!IsHolding && !IsRebond && !IsRolling)
            horizontalMovement = context.ReadValue<Vector2>().x;
        else
            horizontalMovement = 0;
    }

    bool GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer | OneWayLayer))
        {
            jumpsRemaing = maxJumps;
            return true;
        }
        return false;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(jumpsRemaing>0 && !IsHolding && (GroundCheck() || IsRolling) && !IsRebond)
        {
            if(context.performed)
            {
                //Holding jump button  = max height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX , jumpHeight * jumpRuneMultiplier);
                jumpsRemaing --;
                animator.SetTrigger("Jump");
            }
            else if(context.canceled)
            {
                //Tapping jump button = mid height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX , rb.linearVelocityY * 0.5f * jumpRuneMultiplier);
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

    

    private IEnumerator Roulade()
    {
        if (Direction > 0)
            direction  = 1;
        else if (Direction < 0)
            direction = -1;
        duration = 0f;
        if (pressDuration > 1.5)
            pressDuration = 1.5f;
        if (pressDuration < 0.5f)
            pressDuration = 0.5f;
        IsRolling = true;
        while (duration < pressDuration * Rollduration && IsRolling)
        {
            duration += Time.deltaTime;
            rb.linearVelocity = new Vector2(RollSpeed * direction * speedRuneMultiplier * (pressDuration - (duration / Rollduration)) , rb.linearVelocityY);
            jumpsRemaing++;
            yield return null;
        }
        IsRolling = false;
        GroundCheck();
    }

    private IEnumerator Rebond() {
        IsRebond = true;
        float n_duration = 0f;
        rb.linearVelocity = new Vector2(RollSpeed * -direction * speedRuneMultiplier, rb.linearVelocity.y+2*(RollSpeed-Rollduration));
        while (n_duration + duration < pressDuration * Rollduration) {
            n_duration += Time.deltaTime;
            rb.linearVelocity = new Vector2(RollSpeed * -direction * speedRuneMultiplier, rb.linearVelocity.y);
            yield return null;
        }
        GroundCheck();
        IsRebond = false;
        rb.linearVelocity = Vector2.zero;
    }

    void Rune()
    {
        if (runes[1]) // si il y a la rune speed active
            speedRuneMultiplier = addSpeed;
        else
            speedRuneMultiplier = 1f;
        if (runes[0]) // si il y a la rune jump active
            jumpRuneMultiplier = addJump;
        else
            jumpRuneMultiplier = 1f;
        // if (runes[2]) // si il ya la rune levitation active
        //     rb.gravityScale = levitationValue;
        // else
        //     rb.gravityScale = 3f;
        // if (runes[3]) // si il ya la rune light active
        //     rb.gravityScale = levitationValue;
        // else
        //     rb.gravityScale = 3f;
    }

    //Gizmos aren't visible on the game execution (dev tool).
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    void OnTriggerEnter2D( Collider2D coll ) {
        if (IsRolling && (coll.IsTouching(rCol) || coll.IsTouching(lCol))) {
            StopCoroutine(Roulade());
            IsRolling = false;
            StartCoroutine(Rebond());
            //rb.linearVelocity = new Vector2(-(RollSpeed * direction * speedRuneMultiplier * (pressDuration - (duration / Rollduration))), rb.linearVelocityY-10);
        }
    }

}
