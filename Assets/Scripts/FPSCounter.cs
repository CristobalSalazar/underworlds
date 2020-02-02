using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour
{
    private Text _text;
    private int _frameCount;
    private float _time;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _frameCount ++;
        _time += Time.deltaTime;
        if (_time >= 1) {
            _text.text = "fps: " + _frameCount.ToString();
            _time = 0;
            _frameCount = 0;
        }
    }
}
