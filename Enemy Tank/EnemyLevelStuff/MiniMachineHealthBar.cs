using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniMachineHealthBar : MonoBehaviour {

    public static MiniMachineHealthBar Instacnce { private set; get; }

    [SerializeField] private Image healthSpiral;

    public event EventHandler OnLaserShootingTimeActivated;
    public event EventHandler OnLaserShootingTimeDeactivated;

    private bool isLaserShootingActivated = false;

    private float MAXHEALTH = 100f;
    private float currentHealth;

    [SerializeField] private float healthRate = 10f; // units per second

    private bool isDepleting = true; // direction flag

    private void Awake() {
        Instacnce = this;

        currentHealth = MAXHEALTH;
        SetHealthBar();
    }

    private void Update() {
        UpdateHealth();
        SetHealthBar();
    }

    private void UpdateHealth() {
        if (isDepleting) {
            currentHealth -= healthRate * Time.deltaTime;

            if (currentHealth <= 0f) {
                currentHealth = 0f;
                isDepleting = false; // reverse direction

                if (!isLaserShootingActivated) {

                    OnLaserShootingTimeActivated?.Invoke(this, EventArgs.Empty);
                    isLaserShootingActivated = true;
                }

            }
        }
        else {
            currentHealth += healthRate * Time.deltaTime;

            if (currentHealth >= MAXHEALTH) {
                currentHealth = MAXHEALTH;
                isDepleting = true; // reverse direction

                if (isLaserShootingActivated) {

                    OnLaserShootingTimeDeactivated?.Invoke(this, EventArgs.Empty);
                    isLaserShootingActivated = false;
                }


            }
        }
    }

    private float GetNormalizedCurrentHealth() {
        return currentHealth / MAXHEALTH;
    }

    private void SetHealthBar() {
        healthSpiral.fillAmount = GetNormalizedCurrentHealth();
    }
}
