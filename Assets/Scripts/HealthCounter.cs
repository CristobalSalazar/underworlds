using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthCounter : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Text _text;
    private Slider _slider;

    void Start()
    {
        _slider = GetComponent<Slider>();
        GameEvents.On("PlayerDamage", UpdateCounter);
        GameEvents.On("LevelDidLoad", UpdateCounter);
        _slider.value = 1;
    }

    void OnDisable()
    {
        GameEvents.Unsubscribe("PlayerDamage", UpdateCounter);
    }

    // Update is called once per frame
    void UpdateCounter()
    {
        _slider.value = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
        _text.text = _playerHealth.CurrentHealth.ToString() + " / " + _playerHealth.MaxHealth.ToString();
    }
}
