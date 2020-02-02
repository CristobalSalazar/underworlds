using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ManaCounter : MonoBehaviour
{
    [SerializeField] private Text text;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = PlayerMana.CurrentMana / 1000;

        if (text != null)
            text.text = Mathf.Floor(PlayerMana.CurrentMana/100).ToString();
    }
}
