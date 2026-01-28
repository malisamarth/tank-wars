using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
  
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Load the tank selection scene
    }

    public void QuitGame() {
        Application.Quit();
    }

}
