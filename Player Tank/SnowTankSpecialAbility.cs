using UnityEngine;

public class SnowTankSpecialAbility : PlayerTankSpecialAbility {

    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private GameObject iceCannonBallPrefab;
    [SerializeField] private Transform firePoint;

    private float timer = 0f;
    private float cooldown = 5f;
    private bool isSpecialAbility = false;

    public override void SpecialAbilityAtttack() {
        
        if (isSpecialAbility) {
            SpecialCannonBall();
            isSpecialAbility = false;
        }
        else {
            FireNormalCannon();
        }
    }

    private void Update() {
        
        if (timer > cooldown && !isSpecialAbility) {
            timer = 0f;
            isSpecialAbility = true;
        } else {
            timer += Time.deltaTime;
        }

    }

    private void FireNormalCannon() {
        Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
    }

    private void SpecialCannonBall() {
        Instantiate(iceCannonBallPrefab, firePoint.position, firePoint.rotation);
    }

    

}