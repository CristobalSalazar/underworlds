using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float time;
    private float _currentTime;
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = time;
    }


    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
            Destroy(gameObject);
    }
}
