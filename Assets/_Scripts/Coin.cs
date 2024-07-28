using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinMove coinMove;

    private void Awake()
    {
        this.coinMove = GetComponent<CoinMove>();
        this.coinMove.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagnetDetect"))
        {
            Debug.Log("Detect coin");
            this.coinMove.enabled = true;
        }
    }
}
