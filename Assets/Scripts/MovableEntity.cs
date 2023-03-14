using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class MovableEntity : MonoBehaviour
    {
        public float moveSpeed = 3.0f;
        public bool canPush = false;
        public bool debugCollisions = false;

        private Transform entity;
        private Vector2 nextPosition;
        private Vector2Int? nextMovement;

        public UnityEvent<Vector2Int> OnMove;

        private void Start()
        {
            this.entity = gameObject.transform;
            this.nextPosition = this.entity.position;
        }

        public void FixedUpdate()
        {
            if (nextMovement.HasValue)
            {
                var desiredPosition = nextPosition + nextMovement.Value;
                var (canMove, collidingWith) = CanMoveTo(desiredPosition);

                if (canMove)
                {
                    MoveTo(desiredPosition, nextMovement.Value);
                } else
                {
                    if (canPush)
                    {
                        var maybeMovable = collidingWith.GetComponent<MovableEntity>();
                        if (maybeMovable != null && maybeMovable.CanMoveTo(nextMovement.Value).Item1)
                        {
                            maybeMovable.Move(nextMovement.Value);
                            MoveTo(desiredPosition, nextMovement.Value);
                        }
                    }
                }

                nextMovement = null;
            }

            if ((Vector2) entity.position != nextPosition)
            {
                var step = moveSpeed * Time.fixedDeltaTime;
                entity.position = Vector2.MoveTowards(entity.position, nextPosition, step);
            } else
            {
                this.OnMove.Invoke(Vector2Int.zero);
            }
        }

        public (bool, Collider2D) CanMoveTo(Vector2Int movement)
        {
            var positionToEvaluate = nextPosition + movement;
            return CanMoveTo(positionToEvaluate);
        }

        public (bool, Collider2D) CanMoveTo(Vector2 positionToEvaluate)
        {
            if (this.debugCollisions)
            {
                Debug.Log($"{entity.name}: Evaluating collisions at position ({positionToEvaluate.x},{positionToEvaluate.y})");
                Debug.Log($"{entity.name}: Current position ({entity.position.x},{entity.position.y})");
            }

            var collidingWith = Physics2D.OverlapPoint(positionToEvaluate);
            if (collidingWith == null || collidingWith.isTrigger)
            {
                return (true, null);
            }

            if (this.debugCollisions)
            {
                Debug.Log($"{entity.name} will collide with {collidingWith.name}.");
            }

            return (false, collidingWith);
        }

        public void Move(Vector2Int movement)
        {
            var distance = Vector2.Distance(nextPosition, entity.position);

            if (distance <= 0.05f)
            {
                this.nextMovement = movement;
            }
        }

        private void MoveTo(Vector2 nextPosition, Vector2Int movement)
        {
            this.nextPosition = nextPosition;
            OnMove.Invoke(movement);
        }
    }
}