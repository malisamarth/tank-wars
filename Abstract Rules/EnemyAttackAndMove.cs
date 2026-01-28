using UnityEngine;

public abstract class EnemyAttackAndMove : MonoBehaviour
{

    public event System.Action OnEnemyCannonBallFired;

    protected void InvokeOnEnemyCannonBallFire()
    {
        OnEnemyCannonBallFired?.Invoke();
    }

    public abstract void EnemyTankMove();
    public abstract void EnemyTankAttack();
    public abstract void OnEnemyCollision(Collision2D collision);




}
