using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : FusedThrowable
{
    public GameObject explosion;
    public float blastRadius;
    public float blastForce;
    public Condition condition;
    public Condition condition2;
    public float damage;

    public override void Explode ()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);

        foreach(Collider closeObj in collidersToDestroy)
        {
            if (closeObj.GetComponent<Health>() != null)
            {
                closeObj.GetComponent<Health>().TakeDamage(condition, condition2, true, damage);
            }
        }

        Collider[] collidersToForce = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider closeObj in collidersToForce)
        {
            Rigidbody rb = closeObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
    }
}
