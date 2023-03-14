using Assets.Scripts;
using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public MovableEntity Player;

    void Update()
    {
        var horizontalAxis = (int)Input.GetAxisRaw("Horizontal");
        var verticalAxis = (int)Input.GetAxisRaw("Vertical");

        if (Math.Abs(horizontalAxis) == 1)
        {
            var movement = Vector2Int.right * horizontalAxis;
            OnMove(movement);
        } else if (Math.Abs(verticalAxis) == 1)
        {
            var movement = Vector2Int.up * verticalAxis;
            OnMove(movement);
        }
    }

    private void OnMove(Vector2Int movement)
    {
        Player.Move(movement);
    }
}
