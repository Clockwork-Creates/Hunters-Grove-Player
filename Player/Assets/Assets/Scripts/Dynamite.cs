using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : FusedThrowable
{
    public GameObject explosion;

    public override void Explode ()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
