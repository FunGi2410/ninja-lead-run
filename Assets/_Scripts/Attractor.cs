using UnityEngine;

public class Attractor : MonoBehaviour
{
    public string nameTag;
    [SerializeField] float range;

    private void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, this.range);
        foreach(Collider hit in hits)
        {
            if (hit.CompareTag(this.nameTag))
            {
                Vector3 dir = transform.position - hit.transform.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, this.range);
    }
}
