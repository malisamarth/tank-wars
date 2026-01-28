using UnityEngine;

public class BaseEnemyTankMainHealth : EnemyTankHealth
{
    [SerializeField] private float enemyTankFullHealth = 100f;

    public override float EnemyTankFullHealth
    {
        get => enemyTankFullHealth;
        set => enemyTankFullHealth = value;
    }
}
