using UnityEngine;

public class Pieges : MonoBehaviour
{
    public void OnCollisionEnter2D( Collision2D coll ) {
        if (coll.gameObject.tag == "Player")
            Debug.Log("Player dead");
    }
}
