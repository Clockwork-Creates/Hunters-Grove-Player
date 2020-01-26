using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Health
{
    public GameObject destroyedVersion;
    bool isDead;

    public override void Die ()
    {
        if (!isDead)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            isDead = true;
        }
        
        Destroy(gameObject);
    } 
}
