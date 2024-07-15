using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingBoySpawner : MonoBehaviour
{
    [SerializeField] GameObject racingBoyPref;

    private Vector3 randomPosSpawn;

    private Vector3 sizeTile;
    private BoxCollider boxCollider;

    public GameObject tileObj;

    private void Awake()
    {
        this.boxCollider = this.tileObj.GetComponent<BoxCollider>();
        this.sizeTile = this.boxCollider.size;
    }

    private void Start()
    {
        StartCoroutine(SpawnWithTime());
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Spawn();
    }*/

    void Spawn()
    {
        this.randomPosSpawn = new Vector3(Random.Range(-this.sizeTile.x / 2, this.sizeTile.x / 2), 0.05f, transform.position.z);
        GameObject racingBoy = Instantiate(this.racingBoyPref, this.randomPosSpawn, Quaternion.identity);
        //racingBoy.transform.position = this.randomPosSpawn;
    }

    IEnumerator SpawnWithTime()
    {
        Spawn();
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnWithTime());
    }
}
