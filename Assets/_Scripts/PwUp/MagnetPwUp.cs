using UnityEngine;

public class MagnetPwUp : MonoBehaviour, IPowerUp
{
    [SerializeField] private GameObject magnetDetectCollider;
    [SerializeField] private float timeLife;
    public void Collect(Transform parrent)
    {
        // create a magnet detect and child to player
        GameObject magnetColInstance = Instantiate(this.magnetDetectCollider, parrent);
        // set time alive
        Destroy(magnetColInstance, timeLife);
    }
}
