using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] Vector3 directionForce = new Vector3(0f, 1f, -1f);

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Racing"))
        {
            Rigidbody otherRigidbody = collision.rigidbody;

            if (otherRigidbody != null)
            {
                // apply force
                //otherRigidbody.AddExplosionForce(force, transform.position, 10, 10, ForceMode.Impulse);
                Instantiate(this.explosionEffect, collision.transform.position, Quaternion.identity);
                otherRigidbody.AddForce(this.directionForce * force, ForceMode.Impulse);
            }
        }
    }
}
