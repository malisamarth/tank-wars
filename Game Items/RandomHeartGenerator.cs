using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeartGenerator : MonoBehaviour
{

    [SerializeField] private GameObject heartPrefab;

    private PlayerHealthBar playerHealthBar;

    private bool isThereActiveHeartPowerup = true;

    private void OnEnable()
    {
        HeartPoweUpScript.onHeartPowerUpCollected += SpawnHeart;
    }

    private void OnDisable()
    {
        HeartPoweUpScript.onHeartPowerUpCollected -= SpawnHeart;
    }

    private void Start()
    {
        SpwanHeartPowerUp();

        playerHealthBar = PlayerHealthBar.Instance.GetComponent<PlayerHealthBar>();
        playerHealthBar.onPlayerCriticalHealth += PlayerHealthBar_onPlayerCriticalHealth;
    }

    private void Update()
    {
        //Debug.Log(isThereActiveHeartPowerup);
    }

    private void SpawnHeart()
    {
        StartCoroutine(SpwanAfterDelay());
    }

    IEnumerator SpwanAfterDelay()
    {
        isThereActiveHeartPowerup = false;
        yield return new WaitForSeconds(10f);
        SpwanHeartPowerUp();
    }

    private Vector2 randomPositionGenerator()
    {
        float randomSpwanPostionX = Random.Range(-1.7f, 1.74f);
        float randomSpwanPostionY = Random.Range(-4.31f, 4.08f);
        Vector2 randomSpwanCoinPosition = new Vector2(randomSpwanPostionX, randomSpwanPostionY);

        return randomSpwanCoinPosition;
    }
    
    private void PlayerHealthBar_onPlayerCriticalHealth(object sender, System.EventArgs e)
    {
        Debug.Log("Critical Health Reached - Heart Powerup Spawned");

        if (!isThereActiveHeartPowerup)
        {
            SpwanHeartPowerUp();
        }

    }
    private void SpwanHeartPowerUp()
    {
        Instantiate(heartPrefab, randomPositionGenerator(), transform.rotation);
        isThereActiveHeartPowerup = true;
    }

}
