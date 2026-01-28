using System;
using UnityEngine;

public class BaseEnemyTankScript : MonoBehaviour
{
    public static BaseEnemyTankScript Instance { get; private set; }

    private EnemyAttackAndMove enemyAttackAndMove;
    private EnemyTankHealth health;

    public event EventHandler OnHitByPlayerCannonBall;
    public event EventHandler OnCompleteDestructionByPlayer;

    public event EventHandler OnColliedWithPlayerTank;

    private float maxHealth;
    private float currentHealth;

    public float HealthNormalized => currentHealth / maxHealth;

    private void Awake()
    {
        Instance = this;

        enemyAttackAndMove = GetComponentInChildren<EnemyAttackAndMove>();
        health = GetComponentInChildren<EnemyTankHealth>();

        maxHealth = health.EnemyTankFullHealth; 
        currentHealth = maxHealth;              

        Debug.Log($"Enemy Max Health Initialized: {maxHealth}");
    }

    private void Update()
    {
        enemyAttackAndMove.EnemyTankMove();
        enemyAttackAndMove.EnemyTankAttack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyAttackAndMove.OnEnemyCollision(collision);

        OnHitByPlayerCannonBall?.Invoke(this, EventArgs.Empty);

        TakeDamage(10f);

        if (collision.gameObject.TryGetComponent(out PlayerTankMove playerTankMove)) {
            OnColliedWithPlayerTank?.Invoke(this, EventArgs.Empty);  
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log($"Enemy Tank Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnCompleteDestructionByPlayer?.Invoke(this, EventArgs.Empty);
        // Destroy(gameObject);
    }
}
