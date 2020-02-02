using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(Enemy))]
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Enemy _enemy;
    private EnemyHealth _enemyHealth;
    private GameObject _prefab;
    private Slider _healthSlider;

    void OnEnable()
    {
       _enemy = GetComponent<Enemy>();
       _enemyHealth = GetComponent<EnemyHealth>();
       _enemy.Events.On("Damage", ShowHealthBar);
       _enemy.Events.On("Death", OnDeath);
    }

    void OnDisable()
    {
        OnDeath();
       _enemy.Events.Unsubscribe("Damage", ShowHealthBar);
       _enemy.Events.Unsubscribe("Death", OnDeath);
    }

    void Start()
    {
        Transform healthBarTransform = GameObject.Find("EnemyHealthBars").transform;
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
       _prefab = Resources.Load<GameObject>("Prefabs/UI/EnemyHealthBar");
       _prefab = Instantiate(_prefab, pos, _prefab.transform.rotation, healthBarTransform);
       _healthSlider = _prefab.GetComponent<Slider>();
       _prefab.SetActive(false);
    }

    void Update() {
        if (_prefab != null)
            _prefab.transform.position = Camera.main.WorldToScreenPoint(transform.position + _offset);
    }


    void ShowHealthBar()
    {
        _prefab.SetActive(true);
        _healthSlider.value = _enemyHealth.CurrentHealth / _enemyHealth.MaxHealth;

    }

    void OnDeath()
    {
        Destroy(_prefab);
        _prefab = null;
    }
}
