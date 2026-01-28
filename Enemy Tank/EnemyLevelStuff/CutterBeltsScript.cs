using System;
using System.Collections.Generic;
using UnityEngine;

public class CutterBeltsScript : MonoBehaviour {

    public static CutterBeltsScript Instance { get; private set; }

    public event EventHandler<CutterBeltsScriptEventArgs> OnCutterBeltsCompleted;
    public class CutterBeltsScriptEventArgs : EventArgs {

        public int topOrBottom;

        public CutterBeltsScriptEventArgs(int zeroOrOne) {
            topOrBottom = zeroOrOne;
        }

    }

    [SerializeField] private List<Vector3> CutterBeltsChargeAttackPositions;

    private enum AttackType {
        FromTop,
        FromBottom
    }

    [SerializeField] private float speed = 10f;
    private AttackType selectedAttackType;
    private Vector3 currentEndPosition;
    private bool isDistanceReached = true;
    private int top = 1;
    private int bottom = 0;
    private int selectedType;
    private bool isAttackDone = false;

    [SerializeField] private bool fromTop;
    [SerializeField] private bool fromBottom;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        AttackFromTop(); // or AttackFromBottom()
        //AttackFromBottom();

        if (fromTop) {
            fromBottom = false;
            AttackFromTop();
        }

        if (fromBottom) {
            fromTop = false;
            AttackFromBottom();
        }

    }

    private void Update() {
        if (isAttackDone) {
            return;
        }
        MoveCutterBelt();

        if (Vector3.Distance(transform.position, currentEndPosition) < 0.01f) {
            isAttackDone = true;

            if (isDistanceReached) {
                OnCutterBeltsCompleted?.Invoke(this, new CutterBeltsScriptEventArgs(selectedType));
                isDistanceReached = false;
                Destroy(gameObject);
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        //Debug.Log("collided with : " + collision.gameObject.name);

        if (!(collision.gameObject.name == "Level_Four_EnemyTank")) {

            PlayerHealthBar.Instance.CutterDamage();

           /* if (collision.TryGetComponent(out PlayerHealthBar player)) {
                var jump = collision.GetComponent<PlayerJumpHandlerScript>();

                if (jump != null && jump.IsInvincible)
                    return;
            Debug.Log("collided with : " + collision.gameObject.name);

                player.cutterDamage();
            }*/
        }

    }



    private void MoveCutterBelt() {
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentEndPosition,
            speed * Time.deltaTime
        );
    }

    private void AttackFromTop() {
        selectedType = top;
        selectedAttackType = AttackType.FromTop;
        transform.position = CutterBeltsChargeAttackPositions[0];
        currentEndPosition = CutterBeltsChargeAttackPositions[1];
        isAttackDone = false;
    }

    private void AttackFromBottom() {
        selectedType = bottom;
        selectedAttackType = AttackType.FromBottom;
        transform.position = CutterBeltsChargeAttackPositions[1];
        currentEndPosition = CutterBeltsChargeAttackPositions[0];
        isAttackDone = false;
    }
}
