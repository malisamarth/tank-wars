using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IDamageable {

    public static PlayerHealthBar Instance { get; private set; }

    [SerializeField] private ParticleSystem destructionParticleSystem;

    private MiniWarMachineScript miniWarMachineScript;

    public event EventHandler onPlayerCriticalHealth;
    public event EventHandler onGameOver;
    public event EventHandler onHealthLevelReachedSixty;
    public event EventHandler onHealthLevelReachedFourty;
    public event EventHandler onHealthLevelReachedEighty;

    private BaseEnemyTankScript baseEnemyTankScript;

    private PlayerTankMove playerTankMove;
    private SpikesScript spikesScript;


    [SerializeField] private Image healthBar;

    [SerializeField] private float MAXHEALTH = 100f;
    private float currentHealth;
    private float hitpointDamage = 5f;
    private float enemyCollisionHitPoints = 50f;
    private float spikeDamage = 20f;
    private bool isPlayerLost = false;

    private float laserTime = 0;
    private bool isLaserActivated = false;

    private void OnEnable()
    {
        HeartPoweUpScript.onHeartPowerUpCollected += OnPowerUpCollectedMaxOutHealth;
    }

    private void OnDisable()
    {
        HeartPoweUpScript.onHeartPowerUpCollected -= OnPowerUpCollectedMaxOutHealth;
    }

    private void Awake()
    {
        Instance = this;
        currentHealth = MAXHEALTH;
    }

    private void Start() {
        playerTankMove = PlayerTankMove.Instance.GetComponent<PlayerTankMove>();
        playerTankMove.onHitByEnemyCannonBall += PlayerTankMove_onHitByEnemyCannonBall;

        //This is not good, need to fix in next iteration. Fix next update!!!!
        if (SceneManager.GetActiveScene().buildIndex == 6) {
            miniWarMachineScript = MiniWarMachineScript.Instance.GetComponent<MiniWarMachineScript>();
            miniWarMachineScript.OnLaserStartShooting += MiniWarMachineScript_OnLaserStartShooting;
            miniWarMachineScript.OnLaserStopsShooting += MiniWarMachineScript_OnLaserStopsShooting;
        }

        if (SceneManager.GetActiveScene().buildIndex == 3) {
            spikesScript = SpikesScript.Instance.GetComponent<SpikesScript>();
            spikesScript.onSpikeCollision += EnemyTankScript_onSpikeCollision;
        }

;

        baseEnemyTankScript = BaseEnemyTankScript.Instance.GetComponent<BaseEnemyTankScript>();
        baseEnemyTankScript.OnColliedWithPlayerTank += BaseEnemyTankScript_OnColliedWithPlayerTank;
    }

    private void BaseEnemyTankScript_OnColliedWithPlayerTank(object sender, EventArgs e) {
        OnCollisionWithEenmyTank();
    }

    private void MiniWarMachineScript_OnLaserStopsShooting(object sender, EventArgs e) {
        isLaserActivated = false;
    }

    private void MiniWarMachineScript_OnLaserStartShooting(object sender, EventArgs e) {
        isLaserActivated = true;
    }


    private void Update() {
        SetHealthBar();

        if (isLaserActivated) {

            if (laserTime >= 1.4f) {
                LaserDamage();
                laserTime = 0;
            } else {
                laserTime += Time.deltaTime;
            }

        }

    }

    public void TakeDamageByAirplaneBombing(float damage) {
        Debug.Log("CALLED BY: " + new System.Diagnostics.StackTrace());
    }


    public void TakeDamageassByAirplaneBombing(float damage) {

        Debug.Log("planeplane");

        float lastHealth = currentHealth;

        currentHealth -= damage;


        Debug.Log($"{gameObject.name} took {damage} damage");

        Debug.Log("plane damage : " + (lastHealth - currentHealth));
    }

    private void PlayerTankMove_onHitByEnemyCannonBall(object sender, System.EventArgs e) {
        currentHealth -= hitpointDamage;
        //Debug.Log("Player Health: " + currentHealth);
        OnCricitalHealthThreshold();
    }

    private void OnCollisionWithEenmyTank() {
        currentHealth -= enemyCollisionHitPoints;
    }

    private void EnemyTankScript_onSpikeCollision(object sender, System.EventArgs e)
    {
        currentHealth -= spikeDamage;
        //Debug.Log("Player Health: " + currentHealth);
        OnCricitalHealthThreshold();

    }

    private float GetNormalizedCurrentHealth()
    {
        return currentHealth / MAXHEALTH;
    }

    private void SetHealthBar() {
        float healthPercent = currentHealth / MAXHEALTH;

        healthBar.fillAmount = healthPercent;

        // Game over
        if (healthPercent <= 0f) {
            GameOver();
            return;
        }

        // Color logic (percentage based)
        if (healthPercent <= 0.30f) {
            healthBar.color = Color.red;
        }
        else if (healthPercent <= 0.69f) {
            healthBar.color = Color.yellow;
        }
        else {
            healthBar.color = Color.green;
        }

        // Events based on percentage
        if (healthPercent <= 0.60f) {
            onHealthLevelReachedSixty?.Invoke(this, EventArgs.Empty);
        }

        if (healthPercent <= 0.40f) {
            onHealthLevelReachedFourty?.Invoke(this, EventArgs.Empty);
        }

        if (healthPercent >= 0.80f) {
            onHealthLevelReachedEighty?.Invoke(this, EventArgs.Empty);
        }
    }


    private void OnPowerUpCollectedMaxOutHealth()
    {
        currentHealth = MAXHEALTH;
    }

    private void OnCricitalHealthThreshold()
    {
        float cricitalHealthThreshold = 10f;

        if (currentHealth <= cricitalHealthThreshold)
        {
            onPlayerCriticalHealth?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameOver()
    {
        OnPlayerLost();
        onGameOver?.Invoke(this, EventArgs.Empty);
    }


    private void OnPlayerLost()
    {
        if (isPlayerLost)
        {
            return;
        }
        isPlayerLost = true;

        if (destructionParticleSystem != null)
        {
            destructionParticleSystem.Play();

            //Debug.Log("particlesys");

        }

    }

    private void LaserDamage() {

        currentHealth -= 5f;

        Debug.Log("Taking LASER damage!!");

    }

    public void CutterDamage() {
        currentHealth -= 25f;
    }

    public void HeavyRailDamage() {
        currentHealth -= 30f;
    }

}
