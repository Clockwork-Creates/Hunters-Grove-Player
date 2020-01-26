using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandages : Consumable
{
    public float healAmount;

    public override IEnumerator Use()
    {
        yield return new WaitForSeconds(useDelay);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health += healAmount;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().maxhealth += (healAmount / 5);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().UpdateUI();
        anim.SetBool("use", false);
        Destroy(gameObject);
    }
}
