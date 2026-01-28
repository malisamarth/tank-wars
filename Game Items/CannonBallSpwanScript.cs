using System;
using UnityEngine;

public class CannonBallSpwanScript : MonoBehaviour {

    public static CannonBallSpwanScript Instance { get; private set; }

    [SerializeField]
    private PlayerTankSpecialAbility playerTankSpecialAbility;

    public event EventHandler onCannonFired;

    private FireControlButton fireControlButton;

    private void Awake() {

        Instance = this;

        // Auto-pick ANY ability attached to the tank
        if (playerTankSpecialAbility == null) {
            playerTankSpecialAbility = GetComponent<PlayerTankSpecialAbility>();
        }
    }

    private void Start() {
        fireControlButton = FireControlButton.Instance;
        fireControlButton.onFireButtonPressed += FireControlButton_onFireButtonPressed;
    }

    private void FireControlButton_onFireButtonPressed(object sender, EventArgs e) {
        if (playerTankSpecialAbility == null) {
            Debug.LogError("No PlayerTankSpecialAbility found!");
            return;
        }

        playerTankSpecialAbility.SpecialAbilityAtttack();
        onCannonFired?.Invoke(this, EventArgs.Empty);
    }
}
