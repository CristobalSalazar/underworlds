using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public static float CurrentMana { get; private set; }
    [SerializeField] private float manaRechargeRate;

    void Start()
    {
        CurrentMana = 1000;
    }

    public static void DecreaseMana(float amount)
    {
        CurrentMana -= amount;
        CurrentMana = Mathf.Clamp(CurrentMana, 0, 1000);
    }

    void Update()
    {
        CurrentMana += Time.deltaTime * (1 + manaRechargeRate);
        CurrentMana = Mathf.Clamp(CurrentMana, 0, 1000);
    }
}
