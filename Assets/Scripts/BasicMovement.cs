using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    private bool canMove;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movementVector = new Vector3(horizontal, vertical, 0);

        if (movementVector != Vector3.zero) {
            if (canMove && levelGenerator.CanMove(transform.position + movementVector)) {
                transform.position += movementVector;
                canMove = false;
                transform.right = movementVector;
            }
        } else if (movementVector == Vector3.zero && !canMove) {
            canMove = true;
        }
    }
}
