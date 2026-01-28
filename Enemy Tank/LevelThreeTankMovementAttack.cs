using System;
using System.Collections;
using UnityEngine;

public class LevelThreeTankMovementAttack : EnemyAttackAndMove
{

    private PlayerParentScript playerParentScript;
    private GameObject player;

    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private GameObject CannonBallPrefab;
    [SerializeField] private GameObject SpwanPointOne;
    [SerializeField] private GameObject SpwanPointTwo;

    [SerializeField] private GameObject EnemyTankObject;

    public event EventHandler OnHitByPlayerCannonBall;
    public event EventHandler OnHitByIceCannonBall;


    private bool shootingAllowed = false;
    private float nextFireTime;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float stopDistance = 3f;
    [SerializeField] private float fireRate = 1.2f;
    [SerializeField] private float maxYRange = 0.24f;
    [SerializeField] private float followPlayerSpeed = 5f;

    [SerializeField] private GameObject FreezeEffectSprites;

    [SerializeField] private bool specialAttack = false;
    private bool specialAttackDone = false;
    private bool specialAttackInProgress = false;
    private bool AttackedByIceCannonBall = false;


    [SerializeField] private float specialAttackCooldown = 2f;
    private float specialAttackTimer = 0f;

    private void Start() {
        playerParentScript = PlayerParentScript.Instance;
        player = playerParentScript.GetPlayerTankPrefab();

        OnHitByIceCannonBall += (_, __) => AttackedByIceCannonBall = true;
    }

    // -------------------- CORE LOOP --------------------

    public override void EnemyTankMove() {
        if (specialAttackInProgress)
            return;

        HandleSpecialAttackCooldown();

        if (specialAttack)
            StartSpecialAttack();
        else
            NormalAttack();
    }

    public override void EnemyTankAttack() {
        if (!shootingAllowed)
            return;

        shootingAllowed = false;
        Shoot();
    }

    public override void OnEnemyCollision(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out CannonBall _)) {
            OnHitByPlayerCannonBall?.Invoke(this, EventArgs.Empty);
        }

        if (collision.gameObject.TryGetComponent(out IceBallScript _)) {
            OnHitByIceCannonBall?.Invoke(this, EventArgs.Empty);
        }
    }

    // -------------------- NORMAL ATTACK --------------------

    private void NormalAttack() {
        float distance = Vector2.Distance(enemyRigidbody.position, player.transform.position);

        if (distance > attackRange) {
            MoveTowardsPlayer(moveSpeed);
        }
        else if (distance < stopDistance) {
            MoveAwayFromPlayer();
        }
        else {
            ParallelToPlayer();
            shootingAllowed = true;
        }
    }

    // -------------------- SPECIAL ATTACK --------------------

    private void StartSpecialAttack() {
        if (specialAttackInProgress)
            return;

        specialAttackInProgress = true;
        enemyRigidbody.velocity = Vector2.zero;

        StartCoroutine(SpecialAttackRoutine(transform.position, player.transform.position));
    }

    private IEnumerator SpecialAttackRoutine(Vector3 startPosition, Vector3 playerPosition) {
        // -------- PHASE 0: WIND-UP --------
        float windUpTime = 0.25f;
        float windUpSpeed = 4f;
        float elapsed = 0f;

        while (elapsed < windUpTime) {
            if (CheckFreezeInterrupt()) yield break;

            Vector2 backDir = (transform.position - playerPosition).normalized;
            enemyRigidbody.velocity = backDir * windUpSpeed;

            elapsed += Time.deltaTime;
            yield return null;
        }

        enemyRigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.15f);

        // -------- PHASE 1: DASH --------
        float dashTime = 1f;
        float dashSpeed = 20f;
        elapsed = 0f;

        while (elapsed < dashTime) {
            if (CheckFreezeInterrupt()) yield break;

            Vector2 dashDir = (playerPosition - transform.position).normalized;
            enemyRigidbody.velocity = dashDir * dashSpeed;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // -------- PHASE 2: HIT PAUSE --------
        enemyRigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);

        // -------- PHASE 3: RETURN --------
        while (Vector2.Distance(transform.position, startPosition) > 0.1f) {
            if (CheckFreezeInterrupt()) yield break;

            Vector2 returnDir = (startPosition - transform.position).normalized;
            enemyRigidbody.velocity = returnDir * 6f;

            yield return null;
        }

        Shoot();

        ResetSpecialAttack();
    }

    // -------------------- FREEZE LOGIC --------------------

    private bool CheckFreezeInterrupt() {
        if (!AttackedByIceCannonBall)
            return false;

        StartCoroutine(FreezeRoutine());
        return true;
    }

    private IEnumerator FreezeRoutine() {
        enemyRigidbody.velocity = Vector2.zero;
        ShowFreezeEffect();

        yield return new WaitForSeconds(4f);

        ShowDefreezeEffect();
        ResetSpecialAttack();
    }

    // -------------------- HELPERS --------------------

    private void HandleSpecialAttackCooldown() {
        specialAttackTimer += Time.deltaTime;

        if (specialAttackTimer >= specialAttackCooldown) {
            specialAttackTimer = 0f;
            specialAttack = true;
        }
    }

    private void ResetSpecialAttack() {
        enemyRigidbody.velocity = Vector2.zero;
        specialAttack = false;
        specialAttackInProgress = false;
        AttackedByIceCannonBall = false;
    }

    private void MoveTowardsPlayer(float speed) {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        enemyRigidbody.velocity = dir * speed;
    }

    private void MoveAwayFromPlayer() {
        Vector2 dir = (transform.position - player.transform.position).normalized;
        enemyRigidbody.velocity = dir * moveSpeed;
    }

    private void ParallelToPlayer() {
        float targetX = player.transform.position.x;

        float newX = Mathf.Lerp(
            enemyRigidbody.position.x,
            targetX,
            followPlayerSpeed * Time.fixedDeltaTime
        );

        enemyRigidbody.MovePosition(
            new Vector2(newX, enemyRigidbody.position.y)
        );
    }

    private void Shoot() {
        if (Time.time < nextFireTime)
            return;

        Instantiate(CannonBallPrefab, SpwanPointOne.transform.position, SpwanPointOne.transform.rotation);
        InvokeOnEnemyCannonBallFire();

        nextFireTime = Time.time + fireRate;
    }

    private void ShowFreezeEffect() {
        //if (FreezeEffectSprites != null)
        FreezeEffectSprites.SetActive(true);
    }

    private void ShowDefreezeEffect() {
        //if (FreezeEffectSprites != null)
        FreezeEffectSprites.SetActive(false);
    }
}
