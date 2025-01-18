using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] bool[] Selected;
    [SerializeField] List<int> Runes; // 0 => Jump - 1 => Speed - 2 => Lumiere - 3 => Levitation


    public List<int>   Quest_Objects;
    public int Rune_Index = 0;

    public float slowMotionTimeScale = 0.1f;


    void Start() {
        Selected = gameObject.GetComponent<PlayerMovement>().runes;
    }
    public void NextRune( InputAction.CallbackContext context ) {
        if (Runes.Count == 0)
            return ;
        int tmp = Rune_Index;
        if (Rune_Index < Runes.Count)
            Rune_Index++;
        else
            Rune_Index = 0;
        Selected[tmp] = false;
        if (Rune_Index == 0)
            Selected[0] = false;
        else
            Selected[Rune_Index-1] = true;
    }

    public void PrecRune( InputAction.CallbackContext context ) {
        if (Runes.Count == 0)
            return ;
        int tmp = Rune_Index;
        if (Rune_Index > 0)
            Rune_Index--;
        else
            Rune_Index = Runes.Count;
        Selected[tmp] = false;
        if (Rune_Index == 0 )
            Selected[0] = false;
        else
            Selected[Rune_Index-1] = true;
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

    void OnTriggerStay2D( Collider2D coll ) {
        if (coll.CompareTag("Rune") && Input.GetKeyDown(KeyCode.F)) {
            Runes.Add(coll.GetComponent<Id>().id);
            Destroy(coll.gameObject);
            if (Rune_Index == 0)
                Rune_Index = Runes[0];
        }

        if (coll.CompareTag("Quest_Object") && Input.GetKeyDown(KeyCode.F)) {
            Quest_Objects.Add(coll.GetComponent<Id>().id);
            Destroy(coll.gameObject);
        }
    }
}
