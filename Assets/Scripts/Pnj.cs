using UnityEngine;
using UnityEngine.UI;
public class Pnj : MonoBehaviour {

    [SerializeField] Collider2D hitbox;
    [SerializeField] Collider2D chatbox;

    [SerializeField] Text dial;

    [SerializeField] int QuestId;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            if (col.GetComponent<Player>().Quest_Objects.Contains(QuestId)) {
                if (QuestId == 0) {
                    dial.text = "Merci pour mon objet mon pote voila une rune";
                    col.GetComponent<PlayerMovement>().RouladeRune = true;
                }
                else {
                    dial.text = "Merci pour mon objet mon pote voila une rune";
                    col.GetComponent<PlayerMovement>().ChargeRune = true;
                }
                Debug.Log("Play animation");
            }
            if (QuestId == 3 && col.IsTouching(hitbox) && !col.GetComponent<PlayerMovement>().IsRolling) {
                Debug.Log("Player dead");
            }
            if (col.IsTouching(chatbox) && !col.IsTouching(hitbox))
                dial.gameObject.SetActive(true);

            if (col.IsTouching(hitbox) && col.GetComponent<PlayerMovement>().IsRolling) {
                Debug.Log("Pnj Dead");
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player")
            dial.gameObject.SetActive(false);
    }
}
