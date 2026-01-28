using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTankSpecialAbility : PlayerTankSpecialAbility {
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform firePoint;

    public override void SpecialAbilityAtttack() {
        FireNormalCannon();
    }

    private void FireNormalCannon() {
        Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
    }
}