using UnityEngine;

public class Bike : MonoBehaviour
{
    public static Bike Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    [SerializeField] private uint coinNumber;

    public void CollectCoin(uint amount)
    {
        this.coinNumber += amount;
    }
}
