using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D p_Rigidbody2D;
    PlayerAttack scp_PlayerAttack;
    PlayerHealth scp_PlayerHealth;
    PlayerGroundCheck scp_PlayerGroundCheck;

    //Movement
    [Header("PlayerSpeed")]
    [SerializeField] internal float m_StartingPlayerSpeed;
    [SerializeField] internal float m_PlayerSpeed;
    [Header("PlayerJump")]
    [SerializeField] float m_PlayerJumpForce;
    [Header("PlayerDodgeRoll")]
    [SerializeField] float m_RollMultiplier;
    [SerializeField] bool m_IsRolling;
    [SerializeField] float m_JumpTimeCounter;
    [SerializeField] float m_JumpTime;
    //Effect
    //[SerializeField] GameObject vfx_JumpSmoke;
    [Header("Effects")]
    [SerializeField] Animator anim_JumpSmoke,anim_RollSmoke;

    internal bool is_Paused;

    private void Awake()
    {
        p_Rigidbody2D = GetComponent<Rigidbody2D>();
        scp_PlayerAttack = GetComponent<PlayerAttack>();
        scp_PlayerHealth = GetComponent<PlayerHealth>();
        scp_PlayerGroundCheck = FindObjectOfType<PlayerGroundCheck>();
    }

    void Start()
    {
        //GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        ResetMovementStats();//starting default speed
    }
    /*void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }*/
    // Update is called once per frame
    void Update()
    {
        if (!is_Paused)
        {
            if (!scp_PlayerAttack.stats_UsingMeleeAttack && !scp_PlayerHealth.s_isStun)
            {
                Running();
                Jumping();
                DodgeRoll();
            }
        }
    }
    void Running()
    {
        float m_HorizontalMovement = Input.GetAxis("Horizontal");
        //movement
        transform.position += new Vector3(m_HorizontalMovement, 0, 0) * m_PlayerSpeed * Time.fixedDeltaTime;
        if (m_HorizontalMovement >= .1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (m_HorizontalMovement <= -.1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void DodgeRoll()
    {
        float m_HorizontalMovement = Input.GetAxis("Horizontal");
        if (Input.GetButton("DodgeRoll") && Input.GetAxis("Horizontal") !=0 && scp_PlayerGroundCheck.m_IsGrounded)
        {
            transform.position += new Vector3(m_HorizontalMovement, 0, 0) * m_PlayerSpeed * m_RollMultiplier * Time.fixedDeltaTime;
            anim_RollSmoke.SetTrigger("SmokeRoll");
        }
    }
    void Jumping()
    {
        if (Input.GetButtonDown("Jump") && scp_PlayerGroundCheck.m_IsGrounded)
        {
            m_JumpTimeCounter = m_JumpTime;
            p_Rigidbody2D.velocity = Vector2.up * m_PlayerJumpForce;
            //vfx_JumpSmoke.SetActive(true);
            anim_JumpSmoke.SetTrigger("JumpSmoke");
        }
        if (Input.GetKey(KeyCode.Space) && !scp_PlayerGroundCheck.m_IsGrounded)
        {
            if (m_JumpTimeCounter >0)
            {
                p_Rigidbody2D.velocity = Vector2.up * m_PlayerJumpForce;
                m_JumpTimeCounter -= Time.deltaTime;
            }
        }
    }
    internal void UpdateMovementStats(bool Multiply, bool Divide, float valueto)
    {
        if (Multiply)
        {
            m_PlayerSpeed *= valueto;
        }
        if (Divide)
        {
            m_PlayerSpeed /= valueto;
        }
    }
    internal void ResetMovementStats()
    {
        m_PlayerSpeed = m_StartingPlayerSpeed;
    }
}
