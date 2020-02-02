using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EventSystem Events { get; private set; }

    void Awake() {
        Events = new EventSystem();
    }
}
