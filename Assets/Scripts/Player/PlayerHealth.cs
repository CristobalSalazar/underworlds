using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth main { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get { return _maxHealth; } }
    [SerializeField] private float _maxHealth;


    void Start()
    {
        main = this;
        CurrentHealth = _maxHealth;
    }

    public void Damage(float amount)
    {
        Color color = new Color32(0xd9, 0x53, 0x4f, 0xff);
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        UIManager.CreateDamageUI("-" + amount.ToString(), transform.position, color);
        GameEvents.Emit("PlayerDamage");
        StartCoroutine("PlayerDamageRoutine");
        if (CurrentHealth == 0)
            Death();
    }

    IEnumerator PlayerDamageRoutine() {
        int frames = 2;
        while (frames > 0) {
            Time.timeScale = 0f;
            frames --;
            yield return null;
        }
        Time.timeScale = 1;
    }

    private void Death()
    {
        GameEvents.Emit("GameOver");
    }
}
