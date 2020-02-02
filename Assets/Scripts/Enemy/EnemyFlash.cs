using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyFlash : MonoBehaviour
{
    private Enemy _enemy;
    [SerializeField] private SpriteRenderer[] _renderers;
    [SerializeField] private int duration = 1;
    [SerializeField] private Color _flashColor;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.Events.On("Damage", Flash);
    }

    void OnDestroy()
    {
        _enemy.Events.Unsubscribe("Damage", Flash);
    }

    void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        int frames = duration;
        while (frames > 0)
        {
            frames --;
            foreach (SpriteRenderer sr in _renderers)
            {
                sr.color = _flashColor;
            }
            yield return null;
        }

        foreach (SpriteRenderer sr in _renderers)
        {
            sr.color = Color.white;
        }
    }

}

