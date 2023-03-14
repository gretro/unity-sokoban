using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;

        public void Start()
        {
            this.animator = gameObject.GetComponent<Animator>();
        }

        public void OnMove(Vector2Int movement)
        {
            this.animator.SetInteger("Horizontal", movement.x);
            this.animator.SetInteger("Vertical", movement.y);
        }
    }
}