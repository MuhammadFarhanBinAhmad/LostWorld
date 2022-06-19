using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    PlayerHealth scp_PlayerHealth;

    [SerializeField] internal int cp_NumberTag;

    bool in_CheckPoint;
    bool is_Unlocked;

    Animator anim;

    private void Start()
    {
        scp_PlayerHealth = FindObjectOfType<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (in_CheckPoint)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!is_Unlocked)
                {
                    is_Unlocked = true;
                    anim.SetTrigger("Activate");
                }
                scp_PlayerHealth.cp_CurrentNumberTag = cp_NumberTag;
                scp_PlayerHealth.RegainTotalHealth();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            in_CheckPoint = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            in_CheckPoint = false;
        }
    }
}
