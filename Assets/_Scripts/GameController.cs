using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private float maxSpeedBike;
    private float steerSpeedBike;

    public float MaxSpeedBike { get => maxSpeedBike; set => maxSpeedBike = value; }
    public float SteerSpeedBike { get => steerSpeedBike; set => steerSpeedBike = value; }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
