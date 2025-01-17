using UnityEngine;

public class PickUpRunes : MonoBehaviour
{
    public GameObject rune;

    private static int nbDeRune;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player is in range to pick up the rune.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player is out of range.");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (rune != null)
            {
                nbDeRune++;
                Debug.Log("Rune picked up.");
                Destroy(rune);
                isPlayerInRange = false;
                Debug.Log(nbDeRune);
            }
        }
    }
}
