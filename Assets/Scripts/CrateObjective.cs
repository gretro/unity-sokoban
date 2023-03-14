using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CrateObjective : MonoBehaviour, IObjective
    {
        public CrateType crateType;
        public Sprite visual;

        public Sprite Visual { get { return visual; } }

        public string ObjectiveName { 
            get {
                return $"{crateType} Crate objective";
            } 
        }

        public bool IsOnObjective { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.IsTouchingLayers(3))
            {
                var maybeCrate = collision.GetComponent<Crate>();
                if (maybeCrate != null && maybeCrate.crateType == crateType)
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