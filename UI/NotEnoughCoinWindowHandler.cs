using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughCoinWindowHandler : MonoBehaviour {

    [SerializeField] private GameObject NotEnoughCoinsVisual;


    private void Update() {

        Debug.Log("Coins avaliable : " + GameManagerScript.Instance.GetCoins().ToString());


        GameManagerScript.Instance.PrintAllTankData();

    }

    public void SwitchFromNotEnoughCoinsVisual() {
        NotEnoughCoinsVisual.SetActive(false);
    }

}
