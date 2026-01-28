using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandlerScript : MonoBehaviour
{
    [SerializeField] private SelectedPlayerSO selectedPlayerDataSO;

    private LevelNumber selectedLevelNumber;


    public void LoadLevelOne()
    {
        selectedLevelNumber = LevelNumber.Level1;
        SelectedLevelAction(3);
    }

    public void LoadLevelTwo()
    {
        selectedLevelNumber = LevelNumber.Level2;
        SelectedLevelAction(4);
    }

    public void LoadLevelThree()
    {
        selectedLevelNumber = LevelNumber.Level3;
        SelectedLevelAction(5);
    }

    public void LoadLevelFour()
    {
        selectedLevelNumber = LevelNumber.Level4;
        SelectedLevelAction(6);
    }

    public void LoadLevelFive()
    {
        selectedLevelNumber = LevelNumber.Level5;
        SelectedLevelAction(7);
    }

    public void BackToTankSelection()
    {
        SceneManager.LoadScene(1); 
        Debug.Log("Back to Tank Selection");
    }

    private void SelectedLevelAction(int loadSceneNumber)
    {
        selectedPlayerDataSO.selectedLevelNumber = selectedLevelNumber;

        Debug.Log("Selected Level: " + selectedPlayerDataSO.selectedLevelNumber.ToString());

        SceneManager.LoadScene(loadSceneNumber);

    }
}
