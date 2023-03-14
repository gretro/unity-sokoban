using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Crate : MonoBehaviour
    {
        public CrateType crateType;

        public bool IsOnObjective { get; private set; }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.IsTouchingLayers(3))
            {
                var maybeObjective = collision.GetComponent<CrateObjective>();
                if (maybeObjective != null && maybeObjective.crateType == crateType)
                {
                    IsOnObjective = true;
                    Debug.Log($"{gameObject.name}: Is on objective");
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsOnObjective = false;
            Debug.Log($"{gameObject.name}: Is no longer on objective");
        }
    }
}