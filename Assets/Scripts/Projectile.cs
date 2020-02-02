using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float damage;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Random.value < 0.15f) damage *= 2;
        other.gameObject.GetComponent<EnemyHealth>()?.Damage(damage);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
}
