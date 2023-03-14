using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IObjective
    {
        public Sprite Visual { get; }

        public string ObjectiveName { get; }

        public bool IsOnObjective { get; }
    }
}