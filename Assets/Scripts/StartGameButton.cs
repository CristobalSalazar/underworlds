using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour
{
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => {
            SceneManager.LoadScene("Main");
        });
    }
}
