using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get { return _maxHealth; } }
    private Enemy _enemy;
    private GameObject _damageUI;
    private EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        _damageUI = Resources.Load<GameObject>("Prefabs/UI/Damage");
        _enemy = GetComponent<Enemy>();
        CurrentHealth = MaxHealth;
    }

    public void Damage(float amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        if (CurrentHealth <= 0)
        {
            _enemy.Events.Emit("Death");
        } else {
            UIManager.CreateDamageUI(amount.ToString(), transform.position, Color.white);
            _enemy.Events.Emit("Damage");
        }
    }

}
