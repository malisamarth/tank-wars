using UnityEngine;

public class UI_TankScript : MonoBehaviour {

    [SerializeField] private TankType tankType;
    [SerializeField] private int cost;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject lockedVisual;

    private void Start() {
        BuyTankHandler.Instance.OnTankUnlocked += OnTankUnlocked;
        RefreshUI();
    }

    private void OnDestroy() {
        if (BuyTankHandler.Instance != null)
            BuyTankHandler.Instance.OnTankUnlocked -= OnTankUnlocked;
    }



    public void OnBuyButtonClicked() {
        BuyTankHandler.Instance.BuyTank(tankType, cost);
    }

    private void OnTankUnlocked(object sender, BuyTankHandler.OnTankUnlockedEventArgs e) {
        if (e.TankType == tankType) {
            RefreshUI();
        }
    }

    private void RefreshUI() {
        bool unlocked = GameManagerScript.Instance.IsTankUnlocked(tankType);

        Debug.Log($"{tankType} unlocked = {unlocked}");


        lockedVisual.SetActive(!unlocked);
        buyButton.SetActive(!unlocked);
    }
}
