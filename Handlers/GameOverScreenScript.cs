using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenScript : MonoBehaviour
{
    public static GameOverScreenScript Instance { get; private set; }

    public event EventHandler onPlayerLost;

    [SerializeField] private GameObject gameOverScreenObject;
    [SerializeField] private GameObject playerControlObject;

    private PlayerHealthBar playerHealthBar;

    ///private SmoothPauseScript smoothPauseScript;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerHealthBar = PlayerHealthBar.Instance.GetComponent<PlayerHealthBar>();
        playerHealthBar.onGameOver += PlayerHealthBar_onGameOver;

        //smoothPauseScript = SmoothPauseScript.Instance.GetComponent<SmoothPauseScript>();

        HideGameOverScreen();

    }

    private void PlayerHealthBar_onGameOver(object sender, System.EventArgs e)
    {
        LoadGameOverScreen();
    }

    private void LoadGameOverScreen()
    {
        GameResultPause();
        gameOverScreenObject.SetActive(true);
        onPlayerLost?.Invoke(this, EventArgs.Empty);
        playerControlObject.SetActive(false);
    }

    private void HideGameOverScreen()
    {
        Resume();
        gameOverScreenObject.SetActive(false);
        playerControlObject.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        HideGameOverScreen();
        Resume();
    }

    public void GameResultPause() {

        Time.timeScale = 0f;

    }

    public void Resume() {
        Time.timeScale = 1.0f;
    }

    public void ClickPauseResume() {

        if (isPaused) {
            Resume();
            isPaused = false;
        } else {
            GameResultPause();
            isPaused = true;
        }

    }


}
