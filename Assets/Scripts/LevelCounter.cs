using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    private Text text;
    private int currentLevel;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "1";
        currentLevel = 1;
        GameEvents.On("NextLevel", UpdateText);
    }

    void OnDestroy()
    {
        GameEvents.Unsubscribe("NextLevel", UpdateText);
    }

    private void UpdateText()
    {
        currentLevel++;
        text.text = currentLevel.ToString();
    }
}
