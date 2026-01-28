using System;
using UnityEngine;

public class BaseEnemyParticleSystem : MonoBehaviour
{
    private BaseEnemyTankScript baseEnemyTankScript;

    [SerializeField] private ParticleSystem HitByPlayerCannonBall;
    [SerializeField] private ParticleSystem DestructionParticleSystem;

    private bool isEnemyLost = false;

    private void Start()
    {
        baseEnemyTankScript = BaseEnemyTankScript.Instance.GetComponent<BaseEnemyTankScript>();
        baseEnemyTankScript.OnHitByPlayerCannonBall += BaseEnemyTankScript_OnHitByPlayerCannonBall;
        baseEnemyTankScript.OnCompleteDestructionByPlayer += BaseEnemyTankScript_OnCompleteDestrctionByPlayer;
    }

    private void BaseEnemyTankScript_OnCompleteDestrctionByPlayer(object sender, EventArgs e)
    {
        PlayOnEnemyLost();
    }

    private void BaseEnemyTankScript_OnHitByPlayerCannonBall(object sender, System.EventArgs e)
    {
        PlayCannonBallDestructionParticleSystem();
    }

    private void PlayCannonBallDestructionParticleSystem()
    {
        HitByPlayerCannonBall.Play();
    }

    private void PlayOnEnemyLost()
    {
        if (isEnemyLost)
        {
            return;
        }
        isEnemyLost = true;

        if (DestructionParticleSystem != null)
        {
            DestructionParticleSystem.Play();
        }
    }

}
