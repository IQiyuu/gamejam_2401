using UnityEngine;
using UnityEngine.UI;
public class Pnj : MonoBehaviour {

    [SerializeField] Collider2D hitbox;
    [SerializeField] Collider2D chatbox;

    [SerializeField] Text dial;

    [SerializeField] int QuestId;

    public float killDist;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" && QuestId == 3) {
			if (col.IsTouching(hitbox)){
				col.GetComponent<Player>().die();
                dial.gameObject.SetActive(false);
			}
            if (col.GetComponent<Player>().Quest_Objects.Contains(QuestId)) {
                if (QuestId == 0) {
                    dial.text = "Amazing ! Take the Rolling rune. ' C ' to use.";
                    col.GetComponent<PlayerMovement>().RouladeRune = true;
                }
                else {
                    dial.text = "My dude thanks, take this Stomping rune. ' V ' to use.";
                    col.GetComponent<PlayerMovement>().ChargeRune = true;
                }
            }
            if (col.IsTouching(chatbox) && !col.IsTouching(hitbox))
                dial.gameObject.SetActive(true);

            if (col.IsTouching(hitbox) && col.GetComponent<PlayerMovement>().IsRolling)
                Destroy(gameObject);

        }
    }

    void OnTriggerStay2D( Collider2D col ) {
        //if (QuestId == 3)
        //    Debug.Log("Player dead");
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player")
            dial.gameObject.SetActive(false);
    }
}
