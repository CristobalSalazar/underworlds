using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    void OnEnable()
    {
       GameEvents.On("Pause", OnPause);
       GameEvents.On("Resume", OnResume);
    }

    void OnDisable()
    {
        GameEvents.Unsubscribe("Pause", OnPause);
        GameEvents.Unsubscribe("Pause", OnResume);
    }

    void OnPause()
    {
        Time.timeScale = 0;
    }

    void OnResume()
    {
        Time.timeScale = 1;
    }
}
