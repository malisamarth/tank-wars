using UnityEngine;

[CreateAssetMenu(fileName = "SelectedPlayerSO", menuName = "ScriptableObjects/SelectedPlayerSO", order = 1)]
public class SelectedPlayerSO : ScriptableObject
{

    public TankType selectedTank = TankType.None;

    public LevelNumber selectedLevelNumber = LevelNumber.None;

}