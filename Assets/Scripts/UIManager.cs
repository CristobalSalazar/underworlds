using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _healthBars;
    [SerializeField] private Transform _damage;

    public static Transform HealthBars { get; private set; }
    public static Transform Damage { get; private set; }

    void Awake()
    {
        HealthBars = _healthBars;
        Damage = _damage;
    }


    public static void CreateDamageUI(string text, Vector3 worldPosition, Color color)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition + Vector3.up);
        GameObject damagePrefab = Resources.Load<GameObject>("Prefabs/UI/Damage");
        GameObject damageUI = Instantiate(
            damagePrefab,
            screenPosition,
            Quaternion.identity,
            Damage
        );
        Text textComp = damageUI.GetComponent<Text>();
        textComp.text = text;
        textComp.color = color;
    }
}
