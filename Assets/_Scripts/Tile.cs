using UnityEngine;

public class Tile : MonoBehaviour
{
    TileSpawner tileSpawner;

    private void Awake()
    {
        this.tileSpawner = GameObject.FindObjectOfType<TileSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        //print("On exit collision");
        this.tileSpawner.Spawn();
        Destroy(gameObject, 2);
    }
}
