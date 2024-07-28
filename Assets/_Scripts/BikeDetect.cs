using UnityEngine;

public class BikeDetect : MonoBehaviour
{
    private Rigidbody bikeBodyRb;
    private Vector3 dirForce = new Vector3(0f, 1f, -0.5f);
    bool isBreak;
    private void Awake()
    {
        this.bikeBodyRb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Tile"))
        {
            Rigidbody otherRb = collision.rigidbody;

            // calculate relative velocity between 2 objects
            Vector3 relativeVelocity = bikeBodyRb.velocity - collision.relativeVelocity;

            // calculate mass
            float combinedMass = bikeBodyRb.mass * otherRb.mass / (bikeBodyRb.mass + otherRb.mass);

            // Tính toán lực va chạm (lực tác dụng)
            Vector3 impactForce = relativeVelocity * combinedMass;

            Debug.Log("Impact Force: " + impactForce.magnitude + " N");

            if(impactForce.magnitude > 17)
            {
                if (isBreak) return;
                isBreak = true;
                this.bikeBodyRb.freezeRotation = false;
                // apply force
                this.bikeBodyRb.AddForce(impactForce.magnitude * this.dirForce, ForceMode.Impulse);
            }
        }
    }
}
