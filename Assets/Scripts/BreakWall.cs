using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakWall :MonoBehaviour
{
    [SerializeField] AudioSource source;
    void OnCollisionEnter2D( Collision2D coll ) {
        Debug.Log(coll.collider.tag);
        if (coll.collider.tag == "Player" && coll.gameObject.GetComponent<PlayerMovement>().IsRolling) {
            source.Play();
            Destroy(gameObject);
        }
    }
}