using UnityEngine;

public class BreakGround : MonoBehaviour
{
    void OnCollisionEnter2D( Collision2D coll ) {
        Debug.Log(coll.collider.tag);
        if (coll.collider.tag == "Player" && coll.gameObject.GetComponent<PlayerMovement>().IsCharging)
            Destroy(gameObject);
    }
}
