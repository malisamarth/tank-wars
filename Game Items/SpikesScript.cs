using System;
using UnityEngine;

public class SpikesScript : MonoBehaviour {

    public static SpikesScript Instance { get; private set; }

    public event EventHandler onSpikeCollision;

    [SerializeField] private AudioClip spikeCollisionSoundEffect;

    [SerializeField] private Collider2D leftSpikeCollider;
    [SerializeField] private Collider2D rightSpikeCollider;

    private PlayerTankMove playerTankMove;
    private Rigidbody2D playerTankRigidbody2D;

    private WinScreenScript winScreenScript;
    private GameOverScreenScript gameOverScreenScript;

    private bool isScreensActives = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerTankMove = PlayerTankMove.Instance.GetComponent<PlayerTankMove>();
        playerTankRigidbody2D = PlayerTankMove.Instance.GetComponent<Rigidbody2D>();

        winScreenScript = WinScreenScript.Instance.GetComponent<WinScreenScript>();
        winScreenScript.onPlayerWon += WinScreenScript_onPlayerWon;
        gameOverScreenScript = GameOverScreenScript.Instance.GetComponent<GameOverScreenScript>();
        gameOverScreenScript.onPlayerLost += GameOverScreenScript_onPlayerLost;
    }

    private void Update()
    {
        RestTankMassAndMomentum();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {

        if (isScreensActives == false)
        {
            if (collision2D.otherCollider == rightSpikeCollider)
            {
                playerTankRigidbody2D.velocity = new Vector2(+5f, playerTankRigidbody2D.velocity.y);
                onSpikeCollision?.Invoke(this, EventArgs.Empty);

                PlaySpikeCollisionSoundEffect();

                Debug.Log("Left Spike Collision Detected");
            }
            else if (collision2D.otherCollider == leftSpikeCollider)
            {
                playerTankRigidbody2D.velocity = new Vector2(-5f, playerTankRigidbody2D.velocity.y);
                onSpikeCollision?.Invoke(this, EventArgs.Empty);

                PlaySpikeCollisionSoundEffect();

                Debug.Log("Right Spike Collision Detected");
            }
        }

    }

    private void RestTankMassAndMomentum()
    {
        if (playerTankMove.transform.position.x >= -0.73f && playerTankMove.transform.position.x <= 1.14f)
        {
            //playerTankRigidbody2D.velocity = new Vector2(0f, 0f);
        } else
        {
            //playerTankRigidbody2D.velocity = new Vector2(0f, 0f);
        }
    }

    private void WinScreenScript_onPlayerWon(object sender, EventArgs e)
    {
        // Handle player winning the game if needed
        isScreensActives = true;
    }

    private void GameOverScreenScript_onPlayerLost(object sender, EventArgs e)
    {
        // Handle player losing the game if needed
        isScreensActives = true;
    }

    private void PlaySpikeCollisionSoundEffect()
    {
        AudioSource.PlayClipAtPoint(spikeCollisionSoundEffect, transform.position);
    }

}