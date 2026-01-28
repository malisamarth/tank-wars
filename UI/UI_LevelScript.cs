using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UI_LevelScript : MonoBehaviour {

    [SerializeField] GameObject LevelButtton;

    [SerializeField] GameObject LockedLevelSprite;

    [SerializeField] private int leveNumber = 0;


    private void Awake() {

        IsLevelPlayable();

    }

    private void IsLevelPlayable() {
        int unlockedLevelNumber = GameManagerScript.Instance.GetUnlockedLevels();

        if (unlockedLevelNumber >= leveNumber) {
            IsUnlocked();
        }
        else {
            IsLocked();
        }
    }


    private void IsUnlocked() {
        SetUnLockedLevelSprite();
        SetButtonActive();
    }

    private void IsLocked() {
        SetLockedLevelSprite();
        SetButtonDeactivated();
    }

    private void SetLockedLevelSprite() {
        LockedLevelSprite.SetActive(true);
    }

    private void SetUnLockedLevelSprite() {
        LockedLevelSprite.SetActive(false);
    }

    private void SetButtonActive() {
        LevelButtton.SetActive(true);
    }

    private void SetButtonDeactivated() {
        LevelButtton.SetActive(false);
    }

}