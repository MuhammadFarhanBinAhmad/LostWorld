using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorEnemiesAnim : EnemiesAnim
{
    internal void ShieldAnim(bool IsShield)
    {
        anim_EnemyAnimator.SetBool("Shield", IsShield);
    }
}
