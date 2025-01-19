using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakWall :MonoBehaviour
{
    [SerializeField] AudioSource source;
    void OnTriggerEnter2D( Collider2D coll ) {
        Debug.Log(coll.GetComponent<Collider>().tag);
        if (coll.tag == "Player" && coll.gameObject.GetComponent<PlayerMovement>().IsRolling) {
            source.Play();
            Destroy(gameObject);
        }
    }
}