using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float range;
    public float hitDelay;
    public float attackDelay;
    public GameObject metalFX;
    public GameObject stoneFX;
    public GameObject woodFX;
    public GameObject fleshFX;
    public GameObject dirtFX;
    Animator anim;
    Camera fpCam;
    bool canAttack;

    void Start()
    {
        anim = GetComponent<Animator>();
        fpCam = Camera.main;
        canAttack = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack ()
    {
        canAttack = false;

        anim.SetTrigger("attack");

        yield return new WaitForSeconds(hitDelay);

        RaycastHit hit;
        if (Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Wood")
            {
                Instantiate(woodFX, hit.point, Quaternion.LookRotation(hit.normal));
                anim.SetTrigger("blocked");
            }
            if (hit.transform.tag == "Metal")
            {
                Instantiate(metalFX, hit.point, Quaternion.LookRotation(hit.normal));
                anim.SetTrigger("blocked");
            }
            if (hit.transform.tag == "Stone")
            {
                Instantiate(stoneFX, hit.point, Quaternion.LookRotation(hit.normal));
                anim.SetTrigger("blocked");
            }
            if (hit.transform.tag == "Flesh")
            {
                Instantiate(fleshFX, hit.point, Quaternion.LookRotation(hit.normal));
            }
            if (hit.transform.tag == "Dirt")
            {
                Instantiate(dirtFX, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

        yield return new WaitForSeconds(attackDelay - hitDelay);

        canAttack = true;
    }
}
