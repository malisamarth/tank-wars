using UnityEngine;

public class GreenTankSpecialAbility : PlayerTankSpecialAbility {
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform firePoint;

    public override void SpecialAbilityAtttack() {
        FireNormalCannon();
    }

    private void FireNormalCannon() {
        Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
    }
}
