using UnityEngine;

public class LevelTwoTankMainHealth : EnemyTankHealth
{
    [SerializeField] private float enemyTankFullHealth = 100f;

    public override float EnemyTankFullHealth
    {
        get => enemyTankFullHealth;
        set => enemyTankFullHealth = value;
    }
}
