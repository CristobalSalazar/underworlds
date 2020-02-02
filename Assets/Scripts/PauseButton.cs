using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    private bool isPaused = false;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            GameEvents.Emit("Pause");
        } else {
            GameEvents.Emit("Resume");
        }
    }
}
