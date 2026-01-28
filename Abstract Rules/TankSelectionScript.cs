using UnityEngine;
using UnityEngine.SceneManagement;

public class TankSelectionScript : MonoBehaviour
{
    [SerializeField] private SelectedPlayerSO selectedPlayerDataSO;

    private TankType selectedTankType;

    private void Start()
    {
        selectedTankType = TankType.None;
    }

    public void NormalTankSelected ()
    {
        selectedTankType = TankType.NormalTank;
        SelectedTankAction();
    }

    public void MilitaryTankSelected()
    {
        selectedTankType = TankType.MilitaryTank;
        SelectedTankAction();
    }

    public void RedTankSelected()
    {
        selectedTankType = TankType.RedTank;
        SelectedTankAction();
    }

    public void GreenTankSelected()
    {
        selectedTankType = TankType.GreenTank;
        SelectedTankAction();
    }
    public void SnowTankSelected()
    {
        selectedTankType = TankType.SnowTank;
        SelectedTankAction();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Back to the Main Menu");
    }

    private void SelectedTankAction()
    {
        selectedPlayerDataSO.selectedTank = selectedTankType;

        Debug.Log("Selected Tank: " + selectedPlayerDataSO.selectedTank.ToString());

        SceneManager.LoadScene(2); 

    }


}
