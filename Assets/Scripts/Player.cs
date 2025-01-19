using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] bool[] Selected;
    [SerializeField] List<int> Runes; // 0 => Jump - 1 => Speed - 2 => Lumiere - 3 => Levitation


    public List<int>   Quest_Objects;
    public int Rune_Index = 0;

    public float slowMotionTimeScale = 0.1f;

    [SerializeField] AudioSource source;

    [SerializeField] AudioClip right;
    [SerializeField] AudioClip left;

	Vector2 last_checkpoint;

    void Start() {
        Selected = gameObject.GetComponent<PlayerMovement>().runes;
		last_checkpoint = transform.position;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
            NextRune();
        else if (Input.GetKeyDown(KeyCode.Q))
            PrecRune();
    }

    public void NextRune() {
        if (Runes.Count == 0)
            return ;
        int tmp = Rune_Index;
        if (Rune_Index < Runes.Count)
            Rune_Index++;
        else
            Rune_Index = 0;
        Selected[tmp] = false;
        Selected[Rune_Index] = true;
        source.Stop();
        source.clip = right;
        source.Play();
    }

    public void PrecRune() {
        if (Runes.Count == 0)
            return ;
        int tmp = Rune_Index;
        if (Rune_Index > 0)
            Rune_Index--;
        else
            Rune_Index = Runes.Count;
        Selected[tmp] = false;
        Selected[Rune_Index] = true;
        source.Stop();
        source.clip = left;
        source.Play();
    }

    public void WheelRune( InputAction.CallbackContext context ) {
        if (context.performed ) {
            Time.timeScale = slowMotionTimeScale;
            Debug.Log("Open the wheel");
        }
        if (context.canceled) {
            Debug.Log("Close the wheel");
            Time.timeScale = 1;
        }
    }

	public void	die()
	{
		transform.position = last_checkpoint;
		Debug.Log("Player died");
	}

    void OnTriggerStay2D( Collider2D coll ) {
        if (coll.CompareTag("Rune") && Input.GetKeyDown(KeyCode.F)) {
            Runes.Add(coll.GetComponent<Id>().id);
            Destroy(coll.gameObject);
            if (Rune_Index == 0)
                Rune_Index = Runes[0];
			last_checkpoint = coll.transform.position;
        }

        if (coll.CompareTag("Quest_Object") && Input.GetKeyDown(KeyCode.F)) {
            Quest_Objects.Add(coll.GetComponent<Id>().id);
            Destroy(coll.gameObject);
        }

		if (coll.CompareTag("Enemy")){
			die();
		}

		if (coll.CompareTag("Checkpoint")){
			last_checkpoint = coll.transform.position;
		}
    }
}
