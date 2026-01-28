using System;
using UnityEngine;

public class BuyTankHandler : MonoBehaviour {

    public static BuyTankHandler Instance { get; private set; }

    public event EventHandler OnTankPurchaseFailed;

    public event EventHandler<OnTankUnlockedEventArgs> OnTankUnlocked;

    public class OnTankUnlockedEventArgs : EventArgs {
        public TankType TankType;
        public OnTankUnlockedEventArgs(TankType tankType) {
            TankType = tankType;
        }
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void BuyTank(TankType tankType, int cost) {

        int userCoins = GameManagerScript.Instance.GetCoins();

        if (userCoins < cost) {
            Debug.Log("Not enough coins");

            OnTankPurchaseFailed?.Invoke(this, EventArgs.Empty);

            return;
        }

        GameManagerScript.Instance.AddCoins(-cost);
        GameManagerScript.Instance.UnlockTheTank(tankType);

        OnTankUnlocked?.Invoke(this, new OnTankUnlockedEventArgs(tankType));

        //UnlockNextLevel();
    }


    private void UnlockNextLevel() {

        GameManagerScript.Instance.UnlockNextLevel();

    }
}
