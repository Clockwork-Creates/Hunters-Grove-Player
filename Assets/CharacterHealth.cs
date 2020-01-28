using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : Health
{
    public Rigidbody[] bones;
    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    public override void Die()
    {
        foreach(Rigidbody rb in bones)
        {
            rb.isKinematic = false;
            anim.enabled = false;
        }
    }
}
