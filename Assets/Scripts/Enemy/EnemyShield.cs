using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    Animator the_Anim;

    private void Start()
    {
        the_Anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("HitBox"))
        {
            the_Anim.SetTrigger("ShieldHit");
            if (other.GetComponent<Projectile>() != null)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
