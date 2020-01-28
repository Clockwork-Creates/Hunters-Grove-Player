using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusedThrowable : MonoBehaviour
{
    public Transform throwRot;
    public Transform placePos;
    public GameObject cookGO;
    public float throwForce;
    public float explosionDelay;
    public Vector3 offset;
    float remainingDelay;
    bool lit;
    public bool impact;
    Rigidbody rb;
    Camera fpCam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fpCam = Camera.main;
    }

    void Update()
    {
        if (lit)
        {
            remainingDelay -= Time.deltaTime;
            if (remainingDelay <= 0)
            {
                Explode();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Light();
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if (!Input.GetMouseButton(1))
            {
                Throw();
            }
            else
            {
                Place();
            }
        }
    }

    void Light ()
    {
        remainingDelay = explosionDelay;
        lit = true;
        cookGO.SetActive(true);
    }

    void Throw ()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit))
        {
            throwRot.LookAt(hit.point);
        }
        rb.isKinematic = false;
        rb.AddForce(throwRot.forward * throwForce);
        transform.SetParent(null);
    }

    void Place ()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        RaycastHit hit;
        if (Physics.Raycast(placePos.position, -placePos.up + offset, out hit))
        {
            Debug.Log(hit.transform.name);
            transform.position = hit.point;
        }
    }

    void OnCollisionEnter ()
    {
        if (impact)
        {
            Explode();
        }
    }

    public virtual void Explode ()
    {
        Debug.Log("Explode!");
    }
}
