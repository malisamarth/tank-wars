using EasyJoystick;
using System;
using UnityEngine;

public class PlayerTankMove : MonoBehaviour {

    public static PlayerTankMove Instance { private set; get; }

    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D playerTankRigibody2D;

    private Joystick joystickRefer;

    public event EventHandler onHitByEnemyCannonBall;
    public event EventHandler onPlayerMovement;
    public event EventHandler onPlayerMovementIdle;

    private WinScreenScript winScreenScript;
    private GameOverScreenScript gameOverScreenScript;

    private float tankMoveSpeed = 2f;
    private bool isScreensActives = false;

    private Vector3 lastMovement;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        winScreenScript = WinScreenScript.Instance.GetComponent<WinScreenScript>();
        winScreenScript.onPlayerWon += WinScreenScript_onPlayerWon;
        gameOverScreenScript = GameOverScreenScript.Instance.GetComponent<GameOverScreenScript>();
        gameOverScreenScript.onPlayerLost += GameOverScreenScript_onPlayerLost;

        lastMovement = transform.position;

        joystickRefer = Joystick.Instance.GetComponent<Joystick>();

        joystick = joystickRefer;
    }

    private void Update() {
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
            onPlayerMovementIdle?.Invoke(this, EventArgs.Empty);
        }
        
        if (!(lastMovement == transform.position) && (XMovement != 0 && YMovement != 0))
        { 
            //Debug.Log("Movement Detected");
            lastMovement = transform.position;
            onPlayerMovement?.Invoke(this, EventArgs.Empty);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.TryGetComponent(out EnemyCannonBallScript enemyCannonBallScript))
        {
            onHitByEnemyCannonBall?.Invoke(this, EventArgs.Empty);
        }

    }

    private void WinScreenScript_onPlayerWon(object sender, EventArgs e)
    {
        Time.timeScale = 0;
    }

    private void GameOverScreenScript_onPlayerLost(object sender, EventArgs e)
    {
        MakePlayerMovementToStatic();
    }

    private void MakePlayerMovementToStatic()
    {
        playerTankRigibody2D.velocity = Vector2.zero;
    }

}
