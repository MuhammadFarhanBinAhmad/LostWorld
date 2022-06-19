using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerAnim scp_PlayerAnim;
    PlayerHealth_EquipmentUI scp_PlayerHealth_EquipmentUI;
    CheckPointManager scp_CheckPointManager;

    const float s_StartingPlayerHealth = 100;
    public float s_TotalPlayerHealth;
    public float s_CurrentPlayerHealth;
    internal bool is_Dead;

    public int cp_CurrentNumberTag;

    internal bool s_isStun;
    [SerializeField] float s_PushbackForce;

    public float t_StunTime;

    private void Start()
    {
        scp_PlayerAnim = FindObjectOfType<PlayerAnim>();
        scp_PlayerHealth_EquipmentUI = FindObjectOfType<PlayerHealth_EquipmentUI>();
        scp_CheckPointManager = FindObjectOfType<CheckPointManager>();

        //Set starting player health
        s_TotalPlayerHealth = s_StartingPlayerHealth;
        s_CurrentPlayerHealth = s_TotalPlayerHealth;

    }
    internal void TakeDamage(int dmg)
    {
        if (!s_isStun)
        {
            transform.position += new Vector3(-1, 0, 0) * s_PushbackForce * Time.fixedDeltaTime; ;//pushback player
            scp_PlayerAnim.StunAnimation();
            StartCoroutine("StunPeriod");
            s_isStun = true;
            s_CurrentPlayerHealth -= dmg;
            scp_PlayerHealth_EquipmentUI.UpdateHealthBar();
        }    
        if (s_CurrentPlayerHealth <= 0)
        {
            is_Dead = true;
            scp_PlayerAnim.DeathAnimation(is_Dead);
            StartCoroutine("RevivePlayer");
        }
    }
    //for Healthpack and HeatlhDrain Ability
    internal void AddHealth(int health)
    {
        s_CurrentPlayerHealth += health;
        if (s_CurrentPlayerHealth >= s_TotalPlayerHealth)
        {
            s_CurrentPlayerHealth = s_TotalPlayerHealth;
        }
        scp_PlayerHealth_EquipmentUI.UpdateHealthBar();
    }
    internal void RegainTotalHealth()
    {
        s_CurrentPlayerHealth = s_TotalPlayerHealth;
        scp_PlayerHealth_EquipmentUI.UpdateHealthBar();
    }
    //If passive equipment has effect on healthh
    internal void UpdateHealthAmount(bool Multiply,bool Divide, float valueto)
    {
        if (Multiply)
        {
            s_TotalPlayerHealth *= valueto;
            s_CurrentPlayerHealth *= valueto;
        }
        if (Divide)
        {
            s_TotalPlayerHealth /= valueto;
            s_CurrentPlayerHealth /= valueto;
        }
        scp_PlayerHealth_EquipmentUI.UpdateHealthBar();
    }
    //Reset health stats to its original(Before equipment effect) value
    internal void ResetHealthAmount()
    {
        float HealthPercentage = s_CurrentPlayerHealth / s_TotalPlayerHealth;
        s_TotalPlayerHealth = s_StartingPlayerHealth;
        s_CurrentPlayerHealth = s_TotalPlayerHealth * HealthPercentage;//To ensure player health is off correct value when health stats are reset
        scp_PlayerHealth_EquipmentUI.UpdateHealthBar();
    }

    IEnumerator RevivePlayer()
    {
        yield return new WaitForSeconds(3);
        RegainTotalHealth();
        is_Dead = false;
        scp_PlayerAnim.ReviveAnimation(is_Dead);
        transform.position = scp_CheckPointManager.checkpoint_List[cp_CurrentNumberTag].transform.position;
    }
    IEnumerator StunPeriod()
    {
        yield return new WaitForSeconds(t_StunTime);
        s_isStun = false;
    }
}
