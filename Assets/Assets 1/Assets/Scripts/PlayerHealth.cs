using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public float fireDamage;
    public float fireTime;
    float currentFT;
    public float poisonTime;
    float currentPT;
    public float curseTime;
    float currentCT;
    public float bludgeonedTime;
    public float bludgeonedTimeDelay;
    float currentBT;
    public Image healthUI;
    public Image burntHealthUI;
    public Animator cursedImage;
    public Animator poisonImage;
    public Animator damageImage;
    public GameObject fireFX;

    void Update ()
    {
        if (maxhealth>100)
        {
            maxhealth = 100;
        }
        healthUI.fillAmount = health / 100;
        if (burnt && currentFT > 0)
        {
            maxhealth -= fireDamage * Time.deltaTime;
            currentFT -= Time.deltaTime;
            burntHealthUI.fillAmount = (100 - maxhealth) / 100;
            fireFX.SetActive(true);
        }
        else
        {
            fireFX.SetActive(false);
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
        if (poisoned && currentPT > 0)
        {
            currentPT -= Time.deltaTime;
        }
        else
        {
            poisonImage.SetBool("on", false);
        }
        if (cursed && currentCT > 0)
        {
            currentCT -= Time.deltaTime;
        }
        else
        {
            cursedImage.SetBool("on", false);
        }

        if (health > maxhealth)
        {
            health = maxhealth;
        }
    }

    public void UpdateUI()
    {
        burntHealthUI.fillAmount = (100 - maxhealth) / 100;
    }

    public override void TakeDamage(Condition condition, Condition condition2, bool cond2, float damage)
    {
        base.TakeDamage(condition, condition, false, damage);

        damageImage.SetTrigger("flash");

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
            cursedImage.SetBool("on", true);
            currentCT = curseTime;
        }
        if (condition == Condition.Poison)
        {
            poisoned = true;
            poisonImage.SetBool("on", true);
            currentPT = poisonTime;
        }
        if (condition == Condition.Emetic)
        {
            emetic = true;
        }
        // 2nd round
        if (condition2 == Condition.Burning && cond2)
        {
            burnt = true;
            currentFT = fireTime;
        }
        if (condition2 == Condition.Bludgeoning && cond2)
        {
            bludgeoned = true;
            currentBT = bludgeonedTime;
        }
        if (condition2 == Condition.Stunning && cond2)
        {
            stunned = true;
        }
        if (condition2 == Condition.Curse && cond2)
        {
            cursed = true;
            cursedImage.SetBool("on", true);
            currentCT = curseTime;
        }
        if (condition2 == Condition.Poison && cond2)
        {
            poisoned = true;
            poisonImage.SetBool("on", true);
            currentPT = poisonTime;
        }
        if (condition2 == Condition.Emetic && cond2)
        {
            emetic = true;
        }
    }
}
