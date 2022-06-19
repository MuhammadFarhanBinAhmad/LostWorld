using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalMenu : MonoBehaviour
{
    public List<Portal> portal_List;

    public GameObject portal_Menu;
    [SerializeField]internal GameObject go_Player;

    public void TransportPlayer(Portal pos)
    {
        if (pos.portal_Unlocked)
        {
            go_Player.transform.position = pos.transform.position;
        }
    }
    public void OpenMenu()
    {
        portal_Menu.SetActive(true);
    }
    public void CloseMenu()
    {
        portal_Menu.SetActive(false); 
    }
}
