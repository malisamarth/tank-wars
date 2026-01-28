using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionSpriteScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem onEnemyCannonBalldestructionEffect;

    [SerializeField] private GameObject destructionSprite;
    [SerializeField] private GameObject fullHealthSprite;

    private PlayerTankMove playerTankMove;
    private PlayerHealthBar playerHealthBar;

    private void Start()
    {
        playerHealthBar = PlayerHealthBar.Instance.GetComponent<PlayerHealthBar>();
        playerHealthBar.onGameOver += PlayerHealthBar_onGameOver;

        playerTankMove = PlayerTankMove.Instance.GetComponent<PlayerTankMove>();
        playerTankMove.onHitByEnemyCannonBall += PlayerTankMove_onHitByEnemyCannonBall;

        ShowFullHealthSprite();
    }

    private void PlayerHealthBar_onGameOver(object sender, System.EventArgs e)
    {
        ShowDestructionSprite();
    }

    private void ShowDestructionSprite()
    {
        destructionSprite.SetActive(true);
        fullHealthSprite.SetActive(false);
    }

    private void ShowFullHealthSprite()
    {
        destructionSprite.SetActive(false);
        fullHealthSprite.SetActive(true);
    }

    private void PlayerTankMove_onHitByEnemyCannonBall(object sender, System.EventArgs e)
    {
        onEnemyCannonBalldestructionEffect.Play();
    }

}
