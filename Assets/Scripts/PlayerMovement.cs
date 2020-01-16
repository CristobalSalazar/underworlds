using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 0.5f)]
    [SerializeField] private float _movementSpeed;
    public static Vector2 currentPosition;
    private bool _canMove = true;
    private bool _isMoving;
    private Vector3 _targetPosition;

    void Start()
    {
        _targetPosition = transform.position;
    }

    void Update()
    {
        HandleMovement();
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
        currentPosition = position;
        _targetPosition = position;
    }

    private Vector3 GetInputVector()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // remove possibility of diagonal input and prioritize vertical input
        if (horizontal != 0 && vertical != 0) horizontal = 0;
        return new Vector3(horizontal, vertical, 0);

    }

    private bool CanMove(Vector3 targetPosition)
    {
        try {
            GameObject exists = LevelGenerator.Map[(int)targetPosition.x, (int)targetPosition.y];
            return exists != null;
        } catch {
            return false;
        }
    }

    private void HandleMovement()
    {
        Vector3 inputVector = GetInputVector();

        if (inputVector != Vector3.zero) {
            if (_canMove && !_isMoving && CanMove(_targetPosition + inputVector)) {
                _targetPosition += inputVector;
                _canMove = false;
                _isMoving = true;
                transform.right = inputVector;
            }
        }
        if (_isMoving) {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed);
            if (transform.position == _targetPosition) {
                _isMoving = false;
                currentPosition = _targetPosition;
                _canMove = true;
            }
        }
    }

}
