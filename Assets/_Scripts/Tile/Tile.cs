using UnityEngine;

public class Tile : MonoBehaviour
{
    TileSpawner tileSpawner;
    Vector3 nextSpawnPoint;

    private void Awake()
    {
        this.tileSpawner = GameObject.FindObjectOfType<TileSpawner>();
        this.nextSpawnPoint = transform.GetChild(0).transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Bike")) return;
        if(other.transform.position.z > this.nextSpawnPoint.z)
        {
            this.tileSpawner.Spawn();
            Destroy(gameObject, 2);
        }
    }
}
