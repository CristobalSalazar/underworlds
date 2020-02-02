using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : EnemyBaseMovement
{
    [SerializeField] private Vector2 movementDirection;
    private int waitTurns;

    protected override void GetNextPosition()
    {
        if (!canMove || isMoving) return;
        if (waitTurns > 0 )
        {
            waitTurns --;
            return;
        }

        if (PlayerMovement.TargetPosition == TargetPosition + movementDirection) {
            PlayerHealth.main.Damage(Random.Range(1, 3));
            waitTurns ++;
            return;
        }

        Tile tile = LevelGenerator.GetTile(TargetPosition + movementDirection);
        if (tile != null && tile.isSteppable) {
            TargetPosition += movementDirection;
            targetTile.isSteppable = true;
            tile.isSteppable = false;
            targetTile = tile;
        } else {
            movementDirection = -movementDirection;
        }
    }
}
