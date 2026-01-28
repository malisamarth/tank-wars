using System;
using UnityEngine;

public class SelectedTankLoaderScript : MonoBehaviour
{
    [SerializeField] private SelectedPlayerSO selectedPlayerDataSO;
    [SerializeField] private TankPrefabArray[] tankPrefabArray;
    [SerializeField] private GameObject playerSpawnPoint;

    private TankType selectedTankType;

    private void Awake()
    {
        selectedTankType = selectedPlayerDataSO.selectedTank;

        Debug.Log("Loaded Selected Tank: " + selectedTankType.ToString());

        GameObject prefabToSpawn = GetTankPrefab(selectedTankType);

        if (prefabToSpawn == null)
        {
            Debug.LogError("Selected Tank is Not present inside the Array!!");
        }

        Instantiate(prefabToSpawn, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation, playerSpawnPoint.transform);

    }

    private GameObject GetTankPrefab(TankType tankType)
    {
        for (int index = 0; index < tankPrefabArray.Length; index++)
        {
            if (tankPrefabArray[index].tankType == tankType)
            {
                return tankPrefabArray[index].tankPrefab;
            }
        }

        return null;
    }

}
