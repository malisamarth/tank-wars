using System;
using UnityEngine;

public class HeartPoweUpScript : MonoBehaviour
{
    public static event Action onHeartPowerUpCollected;

    [SerializeField] private AudioClip healthPickUpSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerTankMove playerTankMove))
        {
            Debug.Log("Coin Collected");

            onHeartPowerUpCollected?.Invoke();

            OnHealthPickUpSoundEffect();

            Destroy(gameObject);
        }
    }

    private void OnHealthPickUpSoundEffect()
    {
        AudioSource.PlayClipAtPoint(healthPickUpSoundEffect, transform.position);
    }

}
