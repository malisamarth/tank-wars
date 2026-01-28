using System;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    //Global event, this requires no reference needed...better for the the normal publc static
    public static event Action OnCoinCollected;

    [SerializeField] private AudioClip coinPickUpSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerTankMove playerTankMove))
        {
            Debug.Log("Coin Collected");

            GameManagerScript.Instance.AddCoins(1);

            OnCoinCollected?.Invoke();

            OnCoinPickUpSoundEffect();

            Destroy(gameObject);
        }
    }

    private void OnCoinPickUpSoundEffect()
    {
        AudioSource.PlayClipAtPoint(coinPickUpSoundEffect, transform.position);
    }

}
