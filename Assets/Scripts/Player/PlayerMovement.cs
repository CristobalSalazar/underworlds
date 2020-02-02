using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    public static Vector2 TargetPosition { get; private set; }
    public static Vector2 FacingDirection { get; private set; }
    public static Vector2 LastPosition { get; private set; }

    private bool canMove = true;
    private bool isMoving;
    private bool lockMovement;
    private Tile targetTile;
    private int waitTurns = 0;

    void Start()
    {
        TargetPosition = transform.position;
        targetTile = LevelGenerator.GetTile(TargetPosition);
        targetTile.isSteppable = false;

        GameEvents.On("GameOver", () => { lockMovement = true; });
        GameEvents.On("PlayerTick", StartMovement);
    }

    void OnDestroy()
    {
        GameEvents.Unsubscribe("GameOver", () => { lockMovement = true; });
        GameEvents.Unsubscribe("PlayerTick", StartMovement);
    }

    void Update()
    {
        HandleMovement();
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
        LastPosition = position;
        TargetPosition = position;
    }

    private bool CanMove(Vector3 targetPosition)
    {
        Tile tile = LevelGenerator.GetTile(targetPosition);
        if (tile != null && tile.isSteppable)
            return true;
        else
            return false;
    }

    // On Tick
    private void StartMovement()
    {
        if (lockMovement || isMoving) return;

        if (waitTurns > 0)
        {
            waitTurns --;
            return;
        }

        Vector2 inputVector = GameControls.inputDirection;
        if (inputVector.x != 0 && inputVector.y != 0)
            inputVector *= Vector3.up;

        // Start a step
        FacingDirection = inputVector;
        // Event for CharacterController
        GameEvents.Emit("PlayerFace");

        // check tile is a valid tile
        if (CanMove(TargetPosition + inputVector)) {
            // Check for enemy
            targetTile.isSteppable = true;
            targetTile = LevelGenerator.GetTile(TargetPosition + inputVector);
            targetTile.isSteppable = false;

            TargetPosition += inputVector;
            isMoving = true;
            FacingDirection = inputVector;
            GameEvents.Emit("PlayerStartStep");
        }

    }

    private void HandleMovement()
    {
        if (isMoving) {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _movementSpeed * Time.deltaTime);
            if ((Vector2)transform.position == TargetPosition) {
                isMoving = false;
                LastPosition = TargetPosition;
                StaticTile.GetStaticTile(LastPosition)?.Interact(this);
                GameEvents.Emit("PlayerEndStep");
            }
        }
    }

}
