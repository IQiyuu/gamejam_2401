using UnityEngine;

public class Bump : MonoBehaviour
{
    [SerializeField] float padPower;
    private void OnCollisionEnter2D( Collision2D coll ) {
        Collider2D col = coll.collider; 
        if (col.tag == "Player") {
            Rigidbody2D rb = col.attachedRigidbody;
            rb.linearVelocity = new Vector2(rb.linearVelocityX , padPower);
        }
    }
}
