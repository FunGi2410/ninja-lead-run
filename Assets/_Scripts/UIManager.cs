using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    /*------------ PANEL ---------------------*/
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        /*---------- DeActive Panel ----------*/
        this.pausePanel.SetActive(false);
        this.gameOverPanel.SetActive(false);
    }

    public void DisplayPausePanel(bool state)
    {
        this.pausePanel.SetActive(state);
    }

    public void DisplayGameOverPanel(bool state)
    {
        this.gameOverPanel.SetActive(state);
    }
}
