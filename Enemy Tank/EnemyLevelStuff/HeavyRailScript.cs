using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyRailScript : MonoBehaviour { 

    [SerializeField] private List<Vector3> HeavyRailChargeAttackPositions;

    //private CutterBeltsScript CutterBeltsScript;

    private enum AttackType {
        FromTop,
        FromBottom
    }

    private AttackType selectedAttackType;
    private Vector3 currentEndPosition;

    [SerializeField] private float speed = 10f;
    private bool isAttackDone;
    private bool isCutterBeltAttackDone = false;
    private int top = 1;
    private int bottom = 0;
    private int selectedType;

    [SerializeField] private bool fromTop;
    [SerializeField] private bool fromBottom;

    private void Start() {
        //CutterBeltsScript = CutterBeltsScript.Instance.GetComponent<CutterBeltsScript>();
        //CutterBeltsScript.OnCutterBeltsCompleted += CutterBeltsScript_OnCutterBeltsCompleted1; ;


        if (fromTop) {
            fromBottom = false;
            AttackFromTop();
        }

        if (fromBottom) {
            fromTop = false;
            AttackFromBottom();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        //Debug.Log("collided with : " + collision.gameObject.name);

        if (!(collision.gameObject.name == "Level_Four_EnemyTank")) {

            PlayerHealthBar.Instance.HeavyRailDamage();

            /* if (collision.TryGetComponent(out PlayerHealthBar player)) {
                 var jump = collision.GetComponent<PlayerJumpHandlerScript>();

                 if (jump != null && jump.IsInvincible)
                     return;
             Debug.Log("collided with : " + collision.gameObject.name);

                 player.cutterDamage();
             }*/
        }

    }

    /*private void CutterBeltsScript_OnCutterBeltsCompleted1(
        object sender,
        CutterBeltsScript.CutterBeltsScriptEventArgs e) {
        selectedType = e.topOrBottom;
        isCutterBeltAttackDone = true;

        if (selectedType == top) {
            AttackFromTop();
        }
        else if (selectedType == bottom) {
            AttackFromBottom();
        }

        Debug.Log("HeavyRail attack started from: " + selectedType);
    }*/


    private void Update() {
        //if (isAttackDone) return;

        //if (!isCutterBeltAttackDone) return;

        MoveHeavyRail();

        if (Vector3.Distance(transform.position, currentEndPosition) < 0.01f) {
            isAttackDone = true;

            Destroy(gameObject);
        }
    }

    private void MoveHeavyRail() {



        transform.position = Vector3.MoveTowards(
            transform.position,
            currentEndPosition,
            speed * Time.deltaTime
        );
    }

    private void AttackFromTop() {
        selectedAttackType = AttackType.FromTop;
        transform.position = HeavyRailChargeAttackPositions[0];
        currentEndPosition = HeavyRailChargeAttackPositions[1];
        isAttackDone = false;
    }

    private void AttackFromBottom() {
        selectedAttackType = AttackType.FromBottom;
        transform.position = HeavyRailChargeAttackPositions[1];
        currentEndPosition = HeavyRailChargeAttackPositions[0];
        isAttackDone = false;
    }
}
