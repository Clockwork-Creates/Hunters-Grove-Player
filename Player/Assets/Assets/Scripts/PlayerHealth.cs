using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public float fireDamage;
    public float fireTime;
    float currentFT;
    public float bludgeonedTime;
    public float bludgeonedTimeDelay;
    float currentBT;
    public Image healthUI;
    public Image burntHealthUI;

    void Update ()
    {
        healthUI.fillAmount = health / 100;
        if (burnt && currentFT > 0)
        {
            maxhealth -= fireDamage * Time.deltaTime;
            currentFT -= Time.deltaTime;
            burntHealthUI.fillAmount = (100 - maxhealth) / 100;
        }
        if (bludgeoned && currentBT > 0)
        {
            if (currentBT < bludgeonedTime - bludgeonedTimeDelay)
                GetComponent<PlayerController>().canMove = false;
            currentBT -= Time.deltaTime;
        }else if (currentBT <= 0 && currentBT > -1f)
        {
            GetComponent<PlayerController>().canMove = true;
        }

        if (health > maxhealth)
        {
            health = maxhealth;
        }
    }

    public override void TakeDamage(Condition condition, float damage)
    {
        base.TakeDamage(condition, damage);

        if (condition == Condition.Burning)
        {
            burnt = true;
            currentFT = fireTime;
        }
        if (condition == Condition.Bludgeoning)
        {
            bludgeoned = true;
            currentBT = bludgeonedTime;
        }
        if (condition == Condition.Stunning)
        {
            stunned = true;
        }
        if (condition == Condition.Curse)
        {
            cursed = true;
        }
        if (condition == Condition.Poison)
        {
            poisoned = true;
        }
        if (condition == Condition.Emetic)
        {
            emetic = true;
        }
    }
}
