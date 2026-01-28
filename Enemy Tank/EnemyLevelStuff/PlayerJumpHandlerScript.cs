using System.Collections;
using UnityEngine;

public class PlayerJumpHandlerScript : MonoBehaviour {
    private GameObject PlayerTank;
    private PlayerTankMove PlayerTankScript;

    private Vector3 originalTankScale;
    private Vector3 targetScale;

    private Vector3 maxJumpHeight = new Vector3(0.75f, 0.75f, 0.75f);
    private Vector3 invincibleHeight = new Vector3(0.50f, 0.50f, 0.50f);

    [SerializeField] private float scaleSpeed = 8f;

    public bool IsInvincible = false;

    private void Start() {
        PlayerTank = PlayerParentScript.Instance.GetPlayerTankPrefab();
        PlayerTankScript = PlayerTank.GetComponent<PlayerTankMove>();

        originalTankScale = PlayerTank.transform.localScale;
        targetScale = originalTankScale;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.C)) {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            BackToOriginal();
        }

        PlayerTankScript.transform.localScale = Vector3.Lerp(
            PlayerTankScript.transform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
        );

        if (PlayerTankScript.transform.localScale.x <= maxJumpHeight.x &&
            PlayerTankScript.transform.localScale.x > invincibleHeight.x) {

            IsInvincible = true;

            PlayerTankScript.GetComponent<BoxCollider2D>().enabled = false;
        }
        else {
            Debug.Log("Back to Ground !!!!");
            PlayerTankScript.GetComponent<BoxCollider2D>().enabled = true;
            IsInvincible = false;
        }
    }

    public void Jump() {
        targetScale = maxJumpHeight;
        StartCoroutine(BackToGround());
    }

    public void BackToOriginal() {
        targetScale = originalTankScale;
    }

    IEnumerator BackToGround() {
        yield return new WaitForSeconds(0.5f);
        BackToOriginal();
    }
}
