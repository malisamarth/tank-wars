using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectedCoinDisplay : MonoBehaviour {

    private BuyTankHandler BuyTankHandler;

    [SerializeField] private TMP_Text CollectedCoinText;

    private void Start () {
        SetCoinVisual();

        BuyTankHandler = BuyTankHandler.Instance.GetComponent<BuyTankHandler>();
        BuyTankHandler.OnTankUnlocked += BuyTankHandler_OnTankUnlocked;
    }

    private void BuyTankHandler_OnTankUnlocked(object sender, BuyTankHandler.OnTankUnlockedEventArgs e) {

        SetCoinVisual();

        Debug.Log("something has been bought money deducted!!!");

    }

    private void SetCoinVisual() {
        CollectedCoinText.text = GameManagerScript.Instance.GetCoins().ToString();

    }

}