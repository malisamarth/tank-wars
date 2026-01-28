using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class BaseEnemyTankMovementAttack : EnemyAttackAndMove
{

    private PlayerParentScript playerParentScript;
    private GameObject player;
    
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private GameObject CannonBallPrefab;
    [SerializeField] private GameObject SpwanPoint;
    [SerializeField] private GameObject EnemyTankObject;

    [SerializeField] private GameObject FreezeEffectSprites;

    public event EventHandler OnHitByPlayerCannonBall;
    public event EventHandler OnHitByIceCannonBall;

    private bool AttackedByIceCannonBall = false;

    private float moveSpeed = 2f;
    private float attackRange = 5f;
    private float stopDistance = 3f;
    private float fireRate = 1.2f;
    private float maxYRange = 0.24f;
    private float nextFireTime;
    private bool shootingAllowed = false;

    [SerializeField] private float followPlayerSpeed = 2f;



    private void Start()
    {
        playerParentScript = PlayerParentScript.Instance.GetComponent<PlayerParentScript>();
        player = playerParentScript.GetPlayerTankPrefab();
        OnHitByIceCannonBall += BaseEnemyTankMovementAttack_OnHitByIceCannonBall;
    }

    private void BaseEnemyTankMovementAttack_OnHitByIceCannonBall(object sender, EventArgs e) {
        AttackedByIceCannonBall = true;
    }

    public override void EnemyTankMove()
    {
        float distancefromPlayer = Vector2.Distance(enemyRigidbody.position, player.transform.position);


        if (!AttackedByIceCannonBall) {
            if (distancefromPlayer > attackRange /*&& distancefromPlayer >= maxYRange*/)
            {
                MoveTowardsPlayer();

                //Debug.Log("Moving Towardss Player");

            }
            else if (distancefromPlayer < stopDistance)
            {
                MoveAwayFromPlayer();
                //Debug.Log("Moviing Away Player");

            }
            else
            {
                ParallelToPlayer();
                //enemyRigidbody.velocity = Vector2.zero;
                shootingAllowed = true;
                //Debug.Log("Shooot");
            }
        } else {
            StartCoroutine(FreezeYourself(3f));
        }

    }

    IEnumerator FreezeYourself(float freezeTime) {

        ShowFreezeEffect();

        yield return new WaitForSeconds(freezeTime);

        ShowDefreezeEffect();

        AttackedByIceCannonBall = false;

    }


    public override void EnemyTankAttack()
    {
        if (!shootingAllowed)
        {
            return;
        }
        //Debug.Log("Enemy Tank is Attacking");
        shootingAllowed = false;
        Shoot();
    }

    public override void OnEnemyCollision(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {

            OnHitByPlayerCannonBall?.Invoke(this, EventArgs.Empty);
        }

        if (collision.gameObject.TryGetComponent(out IceBallScript iceBallScript)) {

            OnHitByIceCannonBall?.Invoke(this, EventArgs.Empty);

            AttackedByIceCannonBall = true;

        }

    }

    private void MoveTowardsPlayer()
    {
        Vector2 moveDirection = (player.transform.position - transform.position).normalized;
        enemyRigidbody.velocity = moveDirection * moveSpeed;
    }

    private void MoveAwayFromPlayer()
    {

        Vector2 moveDirection = (transform.position - player.transform.position).normalized;
        enemyRigidbody.velocity = moveDirection * moveSpeed;
    }

    private void Shoot()
    {
        if (Time.time < nextFireTime)
        {
            //Do nothing as off now!!!
            return;
        }

        Instantiate(CannonBallPrefab, SpwanPoint.transform.position, SpwanPoint.transform.rotation);
        InvokeOnEnemyCannonBallFire();
        //Debug.Log(">>> SHOOT() CALLED");
        nextFireTime = Time.time + fireRate;
    }

    private void ParallelToPlayer()
    {
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


    private void ShowFreezeEffect() {
        FreezeEffectSprites.SetActive(true);
    }

    private void ShowDefreezeEffect() {
        FreezeEffectSprites.SetActive(false);
    }

}
