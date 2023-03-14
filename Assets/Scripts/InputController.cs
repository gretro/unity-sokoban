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
            Player.Move(Vector2Int.right * horizontalAxis);
        } else if (Math.Abs(verticalAxis) == 1)
        {
            Player.Move(Vector2Int.up * verticalAxis);
        }
    }
}
