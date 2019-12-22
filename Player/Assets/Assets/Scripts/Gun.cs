using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range;
    public float zoom;
    public float nmlzoom;
    float currentzoom = 0;
    public float zoomspeed;
    public int maxAmmo;
    public int mags;
    int ammo;
    public float maxspread;
    public float reloadTime = 1f;
    public GameObject hitFX;
    public ParticleSystem muzzle;
    public PlayerController cont;
    public AudioSource shot;
    public Animator shake;
    public Transform[] shotPoints;
    public GameObject metalFX;
    public GameObject stoneFX;
    public GameObject woodFX;
    public GameObject fleshFX;
    public GameObject dirtFX;
    Animator anim;
    Camera fpCam;

    void Start()
    {
        fpCam = Camera.main;
        anim = GetComponent<Animator>();
        ammo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("Aim", true);
            cont.canNotBeAimed = true;
            currentzoom = Mathf.Lerp(currentzoom, zoom, zoomspeed * Time.deltaTime);
            fpCam.fieldOfView = currentzoom;
        }
        else
        {
            anim.SetBool("Aim", false);
            cont.canNotBeAimed = false;
            currentzoom = Mathf.Lerp(currentzoom, nmlzoom, zoomspeed * Time.deltaTime);
            fpCam.fieldOfView = currentzoom;
        }
        if (Input.GetKeyDown(KeyCode.R) && mags > 0) 
        {
            mags--;
            StartCoroutine(Reload());
        }
        if (ammo <=0)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot ()
    {
        muzzle.Stop();
        muzzle.Play();
        shot.Play();
        shake.SetTrigger("shake");

        ammo--;
        foreach (Transform sp in shotPoints)
        {
            sp.eulerAngles = new Vector3(Random.Range(fpCam.transform.eulerAngles.x - maxspread, fpCam.transform.eulerAngles.x + maxspread), Random.Range(fpCam.transform.eulerAngles.y - maxspread, fpCam.transform.eulerAngles.y + maxspread), 0);
            RaycastHit hit;
            if (Physics.Raycast(sp.transform.position, sp.transform.forward, out hit, range))
            {
                GameObject impact = Instantiate(hitFX, hit.point, Quaternion.LookRotation(hit.normal));
                if (hit.transform.tag == "Wood")
                {
                    Instantiate(woodFX, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (hit.transform.tag == "Metal")
                {
                    Instantiate(metalFX, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (hit.transform.tag == "Stone")
                {
                    Instantiate(stoneFX, hit.point, Quaternion.LookRotation(hit.normal));
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
        }
    }

    IEnumerator  Reload ()
    {
        anim.SetBool("reload", true);
        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("reload", false);
        ammo = maxAmmo;
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        foreach (Transform sp in shotPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(sp.transform.position, sp.transform.forward, out hit, range))
            {
                Gizmos.DrawLine(muzzle.transform.position, hit.point);
            }
        }
    }
}
