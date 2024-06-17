using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    Vector3 nextSpawnPoint;

    private void Start()
    {
        this.Spawn();
        this.Spawn();
        this.Spawn();
        this.Spawn();
    }

    public void Spawn()
    {
        int tileIndex = Random.Range(0, this.tilePrefabs.Length);
        GameObject tile = Instantiate(this.tilePrefabs[tileIndex], this.nextSpawnPoint, Quaternion.identity);
        this.nextSpawnPoint = tile.transform.GetChild(0).transform.position;
    }
}
