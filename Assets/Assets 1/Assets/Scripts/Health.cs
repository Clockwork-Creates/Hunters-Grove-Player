using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxhealth;
    public float health;
    public bool burnt;
    public bool bludgeoned;
    public bool stunned;
    public bool cursed;
    public bool poisoned;
    public bool emetic;

    void Start ()
    {
        health = maxhealth;
    }

    public virtual void TakeDamage(Condition condition, Condition condition2, bool cond2, float damage) //Run this function from other scripts when you want it to take damage
    {
        health -= damage;
        Debug.Log("Taking " + damage + " damage from " + condition);

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die ()
    {
        Debug.Log("Die");
    }
}
