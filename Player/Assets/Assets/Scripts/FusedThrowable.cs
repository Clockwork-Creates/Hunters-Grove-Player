using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusedThrowable : MonoBehaviour
{
    public Transform throwRot;
    public float throwForce;
    public float explosionDelay;
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
            Throw();
        }
    }

    void Light ()
    {
        remainingDelay = explosionDelay;
        lit = true;
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
