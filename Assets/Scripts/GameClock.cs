using UnityEngine;

public class GameClock : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float startTime;
    private float currentTimer;
    public bool PlayerTurn { get; private set; }

    void Start()
    {
        PlayerTurn = true;
        currentTimer = startTime;
    }

    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {

            if (PlayerTurn)
                GameEvents.Emit("PlayerTick");
            else
                GameEvents.Emit("EnemyTick");
            currentTimer = startTime;
            PlayerTurn = !PlayerTurn;
        }
    }
}
