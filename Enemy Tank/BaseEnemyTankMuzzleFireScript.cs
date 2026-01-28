using System.Collections;
using UnityEngine;

public class BaseEnemyTankMuzzleFireScript : MonoBehaviour {
    [SerializeField] private ParticleSystem muzzelFireEffect;

    [SerializeField] private EnemyAttackAndMove enemyAttack;



    private void OnEnable() {
        if (enemyAttack == null) {
            Debug.LogError("EnemyAttackAndMove NOT assigned!");
            return;
        }

        enemyAttack.OnEnemyCannonBallFired += PlayMuzzleEffect;
    }

    private void OnDisable() {
        if (enemyAttack != null) {
            enemyAttack.OnEnemyCannonBallFired -= PlayMuzzleEffect;

        }
    }


    private void PlayMuzzleEffect() {
        StartCoroutine(StopFlash());
    }

    IEnumerator StopFlash() {
        muzzelFireEffect.Play();
        Debug.Log("Muzzel Fire Effect Played");
        yield return new WaitForSeconds(0.25f);
        muzzelFireEffect.Stop();
    }


}
