using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingBoySpawner : MonoBehaviour
{
    [SerializeField] GameObject racingBoyPref;
    [SerializeField] private float timeToSpawn = 4f;
    float firtRacingXAxisPos;

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
        if(Mathf.Abs(this.randomPosSpawn.x - this.firtRacingXAxisPos) < 2)
        {
            this.Spawn();
        }
        else
        {
            GameObject racingBoy = Instantiate(this.racingBoyPref, this.randomPosSpawn, Quaternion.identity);
            this.firtRacingXAxisPos = racingBoy.transform.position.x;
        }
    }

    IEnumerator SpawnWithTime()
    {
        Spawn();
        yield return new WaitForSeconds(this.timeToSpawn);
        StartCoroutine(SpawnWithTime());
    }
}
