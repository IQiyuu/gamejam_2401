using UnityEngine;
using UnityEngine.UI;
public class Pnj : MonoBehaviour {

    [SerializeField] Collider2D hitbox;
    [SerializeField] Collider2D chatbox;

    [SerializeField] Text dial;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            if (col.IsTouching(chatbox) && !col.IsTouching(hitbox))
                dial.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player") {
            if (!col.IsTouching(chatbox))
                dial.gameObject.SetActive(false);
        }
    }
}
