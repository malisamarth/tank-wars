using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemyTankHealth : MonoBehaviour 
{
    public static BaseEnemyTankHealth Instance { get; private set; }

    [SerializeField] private Image fillImage;

    private BaseEnemyTankScript baseEnemyTankScript;

    public event EventHandler onHealthLevelReachedSixty;
    public event EventHandler onHealthLevelReachedFourty;
    public event EventHandler onHealthLevelReachedEighty;
    public event EventHandler onHealthLevelReachedZero;


    private void Awake()
    {
        Instance = this;
        baseEnemyTankScript = GetComponentInParent<BaseEnemyTankScript>();
    }

    private void Update()
    {
        setHealthBar();
    }

    private float GetNormalizedHealth()
    {
        return baseEnemyTankScript.HealthNormalized;
    }

    private void DestructionLevelHealthLevels()
    {
        float currentHealth = GetHealth();

        if (currentHealth <= 0)
        {
            onHealthLevelReachedZero?.Invoke(this, EventArgs.Empty);
        }

        if (currentHealth <= 60)
        {
            onHealthLevelReachedSixty?.Invoke(this, EventArgs.Empty);
        }

        if (currentHealth <= 40)
        {
            onHealthLevelReachedFourty?.Invoke(this, EventArgs.Empty);
        }

        if (currentHealth >= 80)
        {
            onHealthLevelReachedEighty?.Invoke(this, EventArgs.Empty);
        }
    }

    private void setHealthBar()
    {
        fillImage.fillAmount = GetNormalizedHealth();

        DestructionLevelHealthLevels();
    }

    private float GetHealth()
    {
        return baseEnemyTankScript.HealthNormalized * 100f;
    }


}
