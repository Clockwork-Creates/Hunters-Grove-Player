using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartHealth : Health
{
    public Health character;

    public override void Die()
    {
        character.Die();
    }
}
