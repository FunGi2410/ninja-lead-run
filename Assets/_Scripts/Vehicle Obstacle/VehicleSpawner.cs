using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] vehiclePrefabs;
    Vector3 nextSpawnPoint;

    private void Start()
    {
        //this.Spawn();
    }

    public void Spawn(Vector3 spawnPos)
    {
        int index = Random.Range(0, this.vehiclePrefabs.Length);
        GameObject tile = Instantiate(this.vehiclePrefabs[index], spawnPos, Quaternion.identity);
        this.nextSpawnPoint = tile.transform.GetChild(0).transform.position;
    }
}
