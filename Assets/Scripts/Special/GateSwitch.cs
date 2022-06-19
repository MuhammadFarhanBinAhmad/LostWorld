using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{

    public GameObject go_Gate;

    internal bool in_Switch;
    internal bool is_Unlocked;

    public Sprite sprite_SwitchUnlocked;

    private void Update()
    {
        if (in_Switch && !is_Unlocked)
        {
            if (Input.GetButtonDown("Interact"))
            {
                is_Unlocked = true;
                go_Gate.GetComponent<Animator>().SetTrigger("OpenGate");
                GetComponent<SpriteRenderer>().sprite = sprite_SwitchUnlocked;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            in_Switch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            in_Switch = false;
        }
    }
}
