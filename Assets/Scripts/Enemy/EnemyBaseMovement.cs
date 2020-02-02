using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyBaseMovement : MonoBehaviour
{
    private static List<EnemyBaseMovement> enemyMovements = new List<EnemyBaseMovement>();
    public Vector2 TargetPosition { get; protected set; }
    [SerializeField] protected float speed;
    protected bool isMoving;
    protected bool canMove = true;
    protected Enemy enemy;
    protected Tile targetTile;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyMovements.Add(this);
        TargetPosition = transform.position;
        targetTile = LevelGenerator.GetTile(transform.position);
        targetTile.isSteppable = false;
        GameEvents.On("EnemyTick", GetNextPosition);
        enemy.Events.On("Death", OnDeath);

    }

    void OnDisable()
    {
        GameEvents.Unsubscribe("EnemyTick", GetNextPosition);
        enemy.Events.Unsubscribe("Death", OnDeath);
        enemyMovements.Remove(this);
    }

    protected virtual void OnDeath()
    {
        canMove = false;
        targetTile.isSteppable = true;
    }


    void Update()
    {
        if (!canMove) return;
        if ((Vector2)transform.position != TargetPosition) {
            isMoving = true;
            transform.position = Vector3.MoveTowards(
                transform.position,
                TargetPosition,
                speed * Time.deltaTime
            );
        } else {
            isMoving = false;
        }
    }

    protected virtual void GetNextPosition() { }

    public static EnemyBaseMovement GetEnemy (Vector2 pos)
    {
        foreach (EnemyBaseMovement e in enemyMovements)
            if (e.TargetPosition == pos)
                return e;

        return null;
    }
}
