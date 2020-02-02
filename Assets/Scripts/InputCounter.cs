using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class InputCounter : MonoBehaviour
{
    private Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "input: " + GameControls.inputDirection.ToString();

    }

    void Update()
    {
        _text.text = "input: " + GameControls.inputDirection.ToString();
    }
}
