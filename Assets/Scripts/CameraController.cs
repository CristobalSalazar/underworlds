using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,1)]
    [SerializeField] private float followSpeed;
    private Transform follow;
    void Start()
    {
        follow = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lerpVector = Vector2.Lerp(transform.position, follow.position, followSpeed);
        transform.position = new Vector3(lerpVector.x, lerpVector.y, transform.position.z);
    }
}
