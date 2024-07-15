using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private Collider parrentCollider;
    private void Awake()
    {
        this.parrentCollider = transform.parent.gameObject.GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            this.parrentCollider.enabled = false;
            Destroy(transform.parent.gameObject, 3f);
        }
    }
}
