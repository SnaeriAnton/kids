using System;
using UnityEngine;

namespace Game.Contracts
{
    public interface IInput
    {
        public event Action<Vector2> Pressed;
        public event Action<Vector2> Move;
        public event Action<Vector2> Released;
    }
}
