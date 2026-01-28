using UnityEngine;

public class BaseEnemyTankSoundEffectsScript : MonoBehaviour
{
    private BaseEnemyTankHealth baseEnemyTankHealth;
    private BaseEnemyTankScript baseEnemyTankScript;

    [SerializeField] EnemyAttackAndMove enemyAttackAndMove;
    
    [SerializeField] private AudioClip cannonShootSoundEffect;
    [SerializeField] private AudioClip cannonDestroyedSoundEffect;
    [SerializeField] private AudioClip hitByPlayerCannonSoundEffect;



    private void Start()
    {
        baseEnemyTankHealth = BaseEnemyTankHealth.Instance.GetComponent<BaseEnemyTankHealth>();
        baseEnemyTankScript = BaseEnemyTankScript.Instance.GetComponent<BaseEnemyTankScript>();
        baseEnemyTankScript.OnCompleteDestructionByPlayer += BaseEnemyTankScript_OnCompleteDestructionByPlayer;
        baseEnemyTankScript.OnHitByPlayerCannonBall += BaseEnemyTankScript_OnHitByPlayerCannonBall;
    }

    private void BaseEnemyTankScript_OnHitByPlayerCannonBall(object sender, System.EventArgs e)
    {
        OnHitByPlayerCannon();
    }

    private void BaseEnemyTankScript_OnCompleteDestructionByPlayer(object sender, System.EventArgs e)
    {
        Debug.Log("Trying to play Enemy Tank Destroyed Sound Effect Played");
        OnEnemyCannonDestroyed();
    }

    private void OnEnable()
    {
        if (enemyAttackAndMove != null)
        {
            Debug.Log("Its null over here!!");
        }

        //enemyAttackAndMove.OnEnemyCannonBallFired += PlayCannonShootSoundEffect;
    }

    private void OnDisable()
    {
        if (enemyAttackAndMove != null)
        {
            //enemyAttackAndMove.OnEnemyCannonBallFired -= PlayCannonShootSoundEffect;

        }
    }

    private void PlayCannonShootSoundEffect()
    {
        AudioSource.PlayClipAtPoint(cannonShootSoundEffect, transform.position);
    }

    private void OnEnemyCannonDestroyed()
    {
        AudioSource.PlayClipAtPoint(cannonDestroyedSoundEffect, transform.position, 2.0f);
    }

    private void OnHitByPlayerCannon()
    {
        AudioSource.PlayClipAtPoint(hitByPlayerCannonSoundEffect, transform.position);
    }
}
