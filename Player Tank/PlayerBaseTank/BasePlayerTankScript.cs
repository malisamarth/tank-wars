using EasyJoystick;
using System;
using UnityEngine;

public class BasePlayerTankScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerTankRigibody2D;
    [SerializeField] private Joystick joystick;
    private Joystick joystickRefer;

    private float tankMoveSpeed = 2f;

    public event EventHandler OnHitByEnemyCannonBall;
    public event EventHandler OnPlayerMovement;
    public event EventHandler OnPlayerMovementIdle;

    private Vector3 lastMovement;


    private void Awake()
    {
        joystickRefer = Joystick.Instance.GetComponent<Joystick>();
        joystick = joystickRefer;
    }

    private void Update()
    {
        TankJoystickMovement();
    }

    private void TankJoystickMovement()
    {

        float XMovement = joystick.Horizontal();
        float YMovement = joystick.Vertical();

        transform.position += new Vector3(XMovement, YMovement, transform.position.z) * Time.deltaTime * tankMoveSpeed;

        if (XMovement == 0 && YMovement == 0)
        {
            //Debug.Log("No Movement");
            OnPlayerMovementIdle?.Invoke(this, EventArgs.Empty);
        }

        if (!(lastMovement == transform.position) && (XMovement != 0 && YMovement != 0))
        {
            //Debug.Log("Movement Detected");
            lastMovement = transform.position;
            OnPlayerMovement?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent(out EnemyCannonBallScript enemyCannonBallScript))
        {
            OnHitByEnemyCannonBall?.Invoke(this, EventArgs.Empty);
        }


    }

    private void WinScreenScript_onPlayerWon(object sender, EventArgs e)
    {
        // Handle player winning the game if needed
        MakePlayerMovementToStatic();
    }

    private void GameOverScreenScript_onPlayerLost(object sender, EventArgs e)
    {
        // Handle player losing the game if needed
        MakePlayerMovementToStatic();
    }

    private void MakePlayerMovementToStatic()
    {
        playerTankRigibody2D.velocity = Vector2.zero;
    }

}
