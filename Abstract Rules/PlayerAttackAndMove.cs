using UnityEngine;

public abstract class PlayerAttackAndMove : MonoBehaviour
{

    public abstract void PlayerTankMove();
    public abstract void PlayerTankAttack();
    public abstract void OnPlayerTankCollision(Collision2D collision2d);

}
