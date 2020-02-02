using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationOnEvent : MonoBehaviour
{
    [SerializeField] private string _eventName;
    [SerializeField] private string _animationName;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        GameEvents.On(_eventName, PlayAnimation);
    }

    void OnDestroy()
    {
        GameEvents.Unsubscribe(_eventName, PlayAnimation);
    }

    void PlayAnimation()
    {
        _animator.Play(_animationName, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
