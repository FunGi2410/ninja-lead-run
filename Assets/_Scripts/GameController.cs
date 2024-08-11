using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private bool isPauseGame;

    /*private float maxSpeedBike;
    private float steerSpeedBike;

    public float MaxSpeedBike { get => maxSpeedBike; set => maxSpeedBike = value; }
    public float SteerSpeedBike { get => steerSpeedBike; set => steerSpeedBike = value; }*/

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        this.isPauseGame = true;
        Time.timeScale = 0;
        UIManager.instance.DisplayPausePanel(isPauseGame);
    }

    public void ResumeGame()
    {
        this.isPauseGame = false;
        Time.timeScale = 1;
        UIManager.instance.DisplayPausePanel(isPauseGame);
    }

    public void GameOver()
    {
        UIManager.instance.DisplayGameOverPanel(true);
    }
}
