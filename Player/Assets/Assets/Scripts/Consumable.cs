using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float useDelay;
    [HideInInspector]
    public Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Use());
            anim.SetBool("use", true);
        }
    }

    public virtual IEnumerator Use ()
    {
        yield return new WaitForSeconds(useDelay);
    }
}
