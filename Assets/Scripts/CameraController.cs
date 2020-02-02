using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,1)]
    [SerializeField] private float _followSpeed;
    [SerializeField] private Transform _follow;

    void Start()
    {
        if (_follow == null)
            _follow = GameObject.Find("Player").transform;

        GameEvents.On("NextLevel", SnapToFollowPosition);
        GameEvents.On("PlayerDamage", DamageShake);
    }


    private IEnumerator ShakeRoutine(int duration, float strength)
    {
        // get original position
        Vector3 position = transform.position;
        while (duration > 0)
        {
            duration --;
            Vector3 offset = Random.insideUnitSphere * strength;
            offset.z = 0;
            transform.position = position + offset;
            yield return null;
        }
    }

    private void DamageShake()
    {
        StartCoroutine(ShakeRoutine(4, 0.1f));
    }

    void OnDestroy()
    {
        GameEvents.Unsubscribe("NextLevel", SnapToFollowPosition);
        GameEvents.Unsubscribe("PlayerDamage", DamageShake );
    }

    private void SnapToFollowPosition()
    {
        transform.position = new Vector3(_follow.position.x, _follow.position.y, transform.position.z);
    }

    void Update()
    {
        Vector2 lerpVector = Vector2.Lerp(transform.position, _follow.position, _followSpeed);
        transform.position = new Vector3(lerpVector.x, lerpVector.y, transform.position.z);
    }
}
