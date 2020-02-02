using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _restartButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        _exitButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Start");
        });
       GameEvents.On("GameOver", PlayGameOverAnimation);
    }

    void OnDestroy()
    {
        GameEvents.Unsubscribe("GameOver", PlayGameOverAnimation);
    }

    void PlayGameOverAnimation() {
        _animator.enabled = true;
    }
}

