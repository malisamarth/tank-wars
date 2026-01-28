using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RailingSpecialAttackScript : MonoBehaviour {

    [SerializeField] private GameObject CutterBeltsTopBottomPrefab;
    [SerializeField] private GameObject CutterBeltsBottomTopPrefab;
    [SerializeField] private GameObject HeavyRailTopBottomPrefab;
    [SerializeField] private GameObject HeavyRailBottomTopPrefab;

    [Header("Timing")]
    [SerializeField] private float attackCooldown;


    private float attackClockTimer = 0;
    
    private bool isAttackStarted = false;

    private enum AttackType {
        None, FromTop, FromBottom
    }

    private AttackType selectedAttackType = AttackType.None;
    
    private void Update() {

        if (attackClockTimer >= attackCooldown) {
            RandomAttack();
            attackClockTimer = 0;
        } else {
            attackClockTimer += Time.deltaTime;
        }

        Debug.Log("Incoming Attack After : " + attackClockTimer);

        
    }

    private void RandomAttack() {

        int randomValue = UnityEngine.Random.Range(0, 2); // Example: returns 0 or 1

        if (randomValue == 0) {
            Instantiate(CutterBeltsTopBottomPrefab);
            selectedAttackType = AttackType.FromTop;
        }
        else {
            Instantiate(CutterBeltsBottomTopPrefab);
            selectedAttackType = AttackType.FromBottom;
        }

        StartCoroutine(DebugSpawnHeavyRail());

    }

    private IEnumerator DebugSpawnHeavyRail() {
        yield return new WaitForSeconds(2f);

        if (selectedAttackType == AttackType.FromTop) {
            Instantiate(HeavyRailTopBottomPrefab);
        }
        else {
            Instantiate(HeavyRailBottomTopPrefab);
        }

    }

}
