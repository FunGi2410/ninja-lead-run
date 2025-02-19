using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    Vector3 nextSpawnPoint;
    VehicleSpawner vehicleSpawner;

    private void Awake()
    {
        this.vehicleSpawner = GameObject.FindGameObjectWithTag("VehicleSpawner").GetComponent<VehicleSpawner>();
    }
    private void Start()
    {
        this.Spawn();
        this.Spawn();
    }

    public void Spawn()
    {
        int index = Random.Range(0, this.tilePrefabs.Length);
        GameObject tile = Instantiate(this.tilePrefabs[index], this.nextSpawnPoint, Quaternion.identity);
        // Vehicle Spawn
        this.vehicleSpawner.Spawn(this.nextSpawnPoint);
        this.nextSpawnPoint = tile.transform.GetChild(0).transform.position;

        
    }
}
