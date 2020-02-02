using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerWeapon : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private int waitTurns;

    void OnEnable()
    {
        playerMovement = GetComponent<PlayerMovement>();
        GameEvents.On("PlayerTick", CreateWeapon);
    }

    void OnDisable()
    {
        GameEvents.Unsubscribe("PlayerTick", CreateWeapon);
    }

    private void CreateWeapon()
    {
        if (waitTurns > 0)
        {
            waitTurns --;
            return;
        }
        Vector2 searchPosition = PlayerMovement.TargetPosition + PlayerMovement.FacingDirection;
        EnemyBaseMovement e = EnemyBaseMovement.GetEnemy(searchPosition);

        if (e != null)
        {
            EnemyHealth health = e.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.Damage(Random.Range(10, 25));
                waitTurns += 1;
            }
        }
    }

}
