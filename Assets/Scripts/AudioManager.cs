using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager main { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (main != null)
        {
            Destroy(gameObject);
        } else {
            main = this;
            DontDestroyOnLoad(gameObject);
        }


    }
}
