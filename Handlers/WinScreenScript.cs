using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    public static WinScreenScript Instance { get; private set; }

    public event EventHandler onPlayerWon;

    [SerializeField] private GameObject winScreenObject;
    [SerializeField] private GameObject playerControlObject;

    private BaseEnemyTankScript baseEnemyTankScript;

    private SmoothPauseScript smoothPauseScript;


    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        baseEnemyTankScript = BaseEnemyTankScript.Instance.GetComponent<BaseEnemyTankScript>();
        baseEnemyTankScript.OnCompleteDestructionByPlayer += BaseEnemyTankScript_OnCompleteDestrctionByPlayer;

        HideWinScreen();
    }

    private void BaseEnemyTankScript_OnCompleteDestrctionByPlayer(object sender, EventArgs e)
    {
        LoadWinScreen();
    }

    private void LoadWinScreen()
    {
        StartCoroutine(OnPause(2f));

        UnlockNextLevel();

        winScreenObject.SetActive(true);
        onPlayerWon?.Invoke(this, EventArgs.Empty);
        playerControlObject.SetActive(false);
    }

    private void HideWinScreen()
    {
        winScreenObject.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
        Debug.Log("Back to the Main Menu");
    }
  

    IEnumerator OnPause(float waitTime) {

        yield return new WaitForSeconds(waitTime);

        Pause();

        Debug.Log("Are pause after secs");
    }

    public void Pause() {

        Time.timeScale = 0f;

        //smoothPauseScript.SmoothPause();

    }

    public void Resume() {

        Time.timeScale = 1f;

        //smoothPauseScript.Resume();

    }


    private void UnlockNextLevel() {

        GameManagerScript.Instance.UnlockNextLevel();

    }

    public void BackToLevelSelection() {
        SceneManager.LoadScene(2);
    }


}
