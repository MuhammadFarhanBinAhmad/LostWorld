using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PortalMenu scp_PortalMenu;

    bool in_Portal;
    internal bool portal_Unlocked;
    [SerializeField] int portal_TagNumber;

    private void Start()
    {
        scp_PortalMenu = FindObjectOfType<PortalMenu>();
    }

    private void Update()
    {
        if (in_Portal)
        {
            if (Input.GetButtonDown("Interact"))
            {
                scp_PortalMenu.OpenMenu(); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!portal_Unlocked)
        {
            scp_PortalMenu.portal_List[portal_TagNumber].portal_Unlocked = true;
        }
        if (other.tag == "Player")
        {
            in_Portal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            in_Portal = false;
            scp_PortalMenu.CloseMenu();
        }
    }
}
