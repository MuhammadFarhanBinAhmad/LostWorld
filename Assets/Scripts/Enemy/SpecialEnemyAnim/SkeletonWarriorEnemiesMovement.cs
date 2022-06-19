using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorEnemiesMovement : EnemyMovement
{
    internal bool is_Shield;

    [SerializeField]internal SkeletonWarriorEnemiesAnim scp_SkeletonWarriorEnemiesAnim;



    internal override void FixedUpdate()
    {
        MovementPhase();
    }

    internal override void MovementPhase()
    {
        float Distance = Vector2.Distance(transform.position, pos_CurrentPoint.position);
        hit = Physics2D.Raycast(transform.position, raycast, 1);


        //Reach Point
        if (Distance <= .5f)
        {
            if (!em_Stop)
            {
                em_Stop = true;
                scp_EnemiesAnim.RunningAnim(em_Stop);

                em_CurrentMovementState = 1;
                StartCoroutine("StartRest");
            }
        }

        //Contacted Player
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<PlayerHealth>() != null)
            {
                if (!hit.collider.GetComponent<PlayerHealth>().is_Dead)
                {
                    //stop to attack
                    if (!em_Stop)
                    {
                        em_Stop = true;
                        scp_EnemiesAnim.RunningAnim(em_Stop);

                        scp_PlayerHealth = hit.collider.GetComponent<PlayerHealth>();
                        em_CurrentMovementState = 2;
                    }
                }

            }
        }
        else if (scp_PlayerHealth != null)
        {
            scp_PlayerHealth = null;
            StartCoroutine("ContinueMoving");
        }

        //Patrolling
        if (!em_Stop)
        {
            em_CurrentMovementState = 0;
        }

        switch (em_CurrentMovementState)
        {
            //Move
            case 0:
                is_Shield = false;
                scp_SkeletonWarriorEnemiesAnim.ShieldAnim(is_Shield);
                if (!em_Stop)
                {
                    transform.position += transform.right * em_CurrentSpeed * Time.fixedDeltaTime;
                }
                break;
            //Rest
            case 1:
                em_Stop = true;
                is_Shield = true;
                scp_EnemiesAnim.RunningAnim(em_Stop);
                scp_SkeletonWarriorEnemiesAnim.ShieldAnim(is_Shield);

                break;
            //ContactPlayer
            case 2:
                em_Stop = true;
                scp_EnemiesAnim.RunningAnim(em_Stop);
                scp_EnemiesAttack.DetectPlayer();
                break;


        }
    }
}
