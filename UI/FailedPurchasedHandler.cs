using UnityEngine;

public class FailedPurchasedHandler : MonoBehaviour {

    [SerializeField] private GameObject failedPurchasePanel;

    private void Start() {
        BuyTankHandler.Instance.OnTankPurchaseFailed += BuyTankHandler_OnTankPurchaseFailed;
    }

    private void BuyTankHandler_OnTankPurchaseFailed(object sender, System.EventArgs e) {
        ShowFailedPurchasePanel();
    }

    private void ShowFailedPurchasePanel() {
        failedPurchasePanel.SetActive(true);
    }
}
