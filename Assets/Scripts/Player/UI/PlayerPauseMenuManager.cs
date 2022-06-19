using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseMenuManager : MonoBehaviour
{

    [SerializeField] GameObject ui_PauseMenup;


    //"Subscribe" Events
    PlayerAttack scp_PlayerAttack;
    PlayerAnim scp_PlayerAnim;
    PlayerMovement scp_PlayerMovement;

    private void Awake()
    {
        scp_PlayerAttack = FindObjectOfType<PlayerAttack>();
        scp_PlayerAnim = FindObjectOfType<PlayerAnim>();
        scp_PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //pause game+
            GameState currentGameState = GameStateManager.Instance.CurrentGameState;
            GameState newGameState = currentGameState == GameState.Gameplay
                ? GameState.Paused
                : GameState.Gameplay;

            GameStateManager.Instance.SetState(newGameState);

            if (newGameState == GameState.Gameplay)
            {
                scp_PlayerAttack.is_Paused = false;
                scp_PlayerAnim.is_Paused = false;
                scp_PlayerMovement.is_Paused = false;
            }
            if (newGameState == GameState.Paused)
            {
                scp_PlayerAttack.is_Paused = true;
                scp_PlayerAnim.is_Paused = true;
                scp_PlayerMovement.is_Paused = true;
            }

            if (!ui_PauseMenup.activeSelf)
            {
                ui_PauseMenup.SetActive(true);
                return;
            }
            if (ui_PauseMenup.activeSelf)
            {
                ui_PauseMenup.SetActive(false);
                return;
            }
        }
    }
}
