using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float range;
    public float hitDelay;
    public float attackDelay;
    public float attackForce;
    public float damage;
    public GameObject metalFX;
    public GameObject stoneFX;
    public GameObject woodFX;
    public GameObject fleshFX;
    public GameObject dirtFX;
    public Condition condition;
    public LayerMask raycastLayers;
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
        if (Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit, range, raycastLayers))
        {
            if (hit.transform.GetComponent<Health>() != null)
            {
                hit.transform.GetComponent<Health>().TakeDamage(condition, condition, false, damage);
            }

            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(fpCam.transform.forward * attackForce);
            }

            if (hit.transform.tag == "Wood")
            {
                GameObject instGO = Instantiate(woodFX, hit.point, Quaternion.LookRotation(hit.normal));
                instGO.transform.SetParent(hit.transform);
                anim.SetTrigger("blocked");
            }
            if (hit.transform.tag == "Metal")
            {
                GameObject instGO = Instantiate(metalFX, hit.point, Quaternion.LookRotation(hit.normal));
                instGO.transform.SetParent(hit.transform);
                anim.SetTrigger("blocked");
            }
            if (hit.transform.tag == "Stone")
            {
                GameObject instGO = Instantiate(stoneFX, hit.point, Quaternion.LookRotation(hit.normal));
                instGO.transform.SetParent(hit.transform);
                anim.SetTrigger("blocked");
            }
            if (hit.transform.tag == "Flesh")
            {
                GameObject instGO = Instantiate(fleshFX, hit.point, Quaternion.LookRotation(hit.normal));
                instGO.transform.SetParent(hit.transform);
            }
            if (hit.transform.tag == "Dirt")
            {
                GameObject instGO = Instantiate(dirtFX, hit.point, Quaternion.LookRotation(hit.normal));
                instGO.transform.SetParent(hit.transform);
            }
        }

        yield return new WaitForSeconds(attackDelay - hitDelay);

        canAttack = true;
    }
}
