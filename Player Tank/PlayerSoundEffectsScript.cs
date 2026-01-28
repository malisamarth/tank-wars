using UnityEngine;

public class PlayerSoundEffectsScript : MonoBehaviour
{
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioClip cannonShootSoundEffect;
    [SerializeField] private AudioClip cannonDestroyedSoundEffect;
    [SerializeField] private AudioClip hitByEnemyCannonSoundEffect;


    private PlayerTankMove playerTankMove;
    private CannonBallSpwanScript cannonBallSpwanScript;
    private PlayerHealthBar playerHealthBar;    

    private bool isPlayerDestroyedSoundPlayed = false;

    private void Start()
    {
        OnEngineStartIdleSound();

        cannonBallSpwanScript = CannonBallSpwanScript.Instance;
        cannonBallSpwanScript.onCannonFired += CannonBallSpwanScript_onCannonFired; ;

        playerHealthBar = PlayerHealthBar.Instance.GetComponent<PlayerHealthBar>();
        playerHealthBar.onGameOver += PlayerHealthBar_onGameOver;

        playerTankMove = PlayerTankMove.Instance.GetComponent<PlayerTankMove>();
        playerTankMove.onPlayerMovement += PlayerTankMove_onPlayerMovement;
        playerTankMove.onPlayerMovementIdle += PlayerTankMove_onPlayerMovementIdle;
        playerTankMove.onHitByEnemyCannonBall += PlayerTankMove_onHitByEnemyCannonBall;
    }

    private void PlayerTankMove_onHitByEnemyCannonBall(object sender, System.EventArgs e)
    {
        OnHitByEnemyCannon();
    }

    private void PlayerHealthBar_onGameOver(object sender, System.EventArgs e)
    {
        PauseAllSoundEffects();
        OnPlayerCannonDestroyed();
    }

    private void CannonBallSpwanScript_onCannonFired(object sender, System.EventArgs e)
    {
        PlayCannonShootSoundEffect();
    }


    private void PlayCannonShootSoundEffect()
    {
        AudioSource.PlayClipAtPoint(cannonShootSoundEffect, transform.position);
    }

    private void OnPlayerCannonDestroyed()
    {
        if (!isPlayerDestroyedSoundPlayed)
        {
            AudioSource.PlayClipAtPoint(cannonDestroyedSoundEffect, transform.position, 2.0f);
            isPlayerDestroyedSoundPlayed = true;
        }

    }

    private void PlayerTankMove_onPlayerMovement(object sender, System.EventArgs e)
    {
        OnEngineMovementSound();
    }
    private void PlayerTankMove_onPlayerMovementIdle(object sender, System.EventArgs e)
    {
        OnEngineIdleAfterMovingSound();
    }

    private void OnEngineStartIdleSound()
    {
        engineAudio.volume = 0.01f;
        engineAudio.pitch = 1f;
    }

    private void OnEngineIdleAfterMovingSound()
    {
        float idleVolume = 0.01f;
        float minPitch = 1f;
        float pitchingSpeed = 3f;

        engineAudio.pitch = Mathf.Lerp(
            engineAudio.pitch,
            minPitch,
            Time.deltaTime * pitchingSpeed
        );

        engineAudio.volume = Mathf.Lerp(
            engineAudio.volume,
            idleVolume,
            Time.deltaTime * pitchingSpeed
        );

    }

    private void OnEngineMovementSound()
    {
        float movementVolume = 0.04f;
        float maxPitch = 2.2f;
        float pitchingSpeed = 5f;

        engineAudio.pitch = Mathf.Lerp(
            engineAudio.pitch,
            maxPitch,
            Time.deltaTime * pitchingSpeed
        );

        engineAudio.volume = Mathf.Lerp(
            engineAudio.volume,
            movementVolume,
            Time.deltaTime * pitchingSpeed
        );

    }

    private void OnHitByEnemyCannon()
    {
        AudioSource.PlayClipAtPoint(hitByEnemyCannonSoundEffect, transform.position);
    }


    private void PauseAllSoundEffects()
    {
        engineAudio.volume = 0;
    }

}