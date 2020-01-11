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
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        if (movement != Vector3.zero && canMove && levelGenerator.CanMove(transform.position + movement)) {
            transform.position += movement;
            canMove = false;
        } else if (movement == Vector3.zero && !canMove) {
            canMove = true;
        }
    }
}
