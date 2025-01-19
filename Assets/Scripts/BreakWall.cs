using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakWall :MonoBehaviour
{
    [SerializeField] AudioSource source;
    void OnTriggerEnter2D( Collider2D coll ) {
        if (coll.tag == "Player" && coll.gameObject.GetComponent<PlayerMovement>().IsRolling) {
            source.Play();
            Destroy(gameObject);
        }
    }
}