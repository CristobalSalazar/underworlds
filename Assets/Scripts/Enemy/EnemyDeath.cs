using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject destroyFX;
    [SerializeField] private SpriteRenderer _fadeSprite;
    private Enemy _enemy;


    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.Events.On("Death", OnDeath);
    }

    void OnDeath()
    {
        if (destroyFX != null)
            Instantiate(destroyFX, transform.position, destroyFX.transform.rotation, null);

        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        if (_fadeSprite != null)
        {
            while (_fadeSprite.color.a > 0)
            {
                _fadeSprite.color = new Color(
                    _fadeSprite.color.r,
                    _fadeSprite.color.g,
                    _fadeSprite.color.b,
                    Mathf.Max(_fadeSprite.color.a - (Time.deltaTime * 2), 0)
                );
                yield return null;
            }
        }

        Destroy(gameObject);

    }
}
