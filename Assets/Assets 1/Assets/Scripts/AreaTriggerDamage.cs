using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerDamage : MonoBehaviour
{
    public Condition condition;
    public float damage;

    void OnTriggerEnter (Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(condition, condition, false, damage);
        }
    }
}

public enum Condition { Burning, Curse, Poison, Bleeding, Piecing, Bludgeoning, Emetic, Sedative, InstantKill, Stunning}
