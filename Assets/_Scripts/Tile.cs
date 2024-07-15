using UnityEngine;

public class Tile : MonoBehaviour
{
    /*public delegate void VehicleActionEvent();
    public static event VehicleActionEvent VehicleAction;*/

    [SerializeField] private bool isBikeArrive;

    TileSpawner tileSpawner;

    public bool IsBikeArrive { get => isBikeArrive; }

    private void Awake()
    {
        this.tileSpawner = GameObject.FindObjectOfType<TileSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.isBikeArrive = true;
        //print(other.name + " Collded");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Bike")) return;
        this.tileSpawner.Spawn();
        Destroy(gameObject, 2);
    }
}
