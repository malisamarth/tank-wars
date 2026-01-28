using System;
using UnityEngine;

public class PlayerParentScript : MonoBehaviour
{
    public static PlayerParentScript Instance { private set; get; }

    public event EventHandler<OnPlayerTankAcquiredEventArgs> OnPlayerTankAcquired;
    public class OnPlayerTankAcquiredEventArgs : EventArgs
    {
        public OnPlayerTankAcquiredEventArgs(GameObject playerTankPrefab)
        {
            PlayerTankPrefab = playerTankPrefab;
        }

        public GameObject PlayerTankPrefab;
    }

    private GameObject playerTankChildPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTransformChildrenChanged()
    {
        if (playerTankChildPrefab != null) return;

        playerTankChildPrefab = transform.GetChild(0).gameObject;
        OnPlayerTankAcquired?.Invoke(this, new OnPlayerTankAcquiredEventArgs(playerTankChildPrefab));
        Debug.Log("Tank GameObject acquired: " + playerTankChildPrefab.name);
    }

    public GameObject GetPlayerTankPrefab()
    {
        return playerTankChildPrefab;
    }

}
