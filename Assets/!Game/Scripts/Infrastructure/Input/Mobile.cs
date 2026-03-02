using System;
using UnityEngine;

namespace Game.InputSystem
{
    public class Mobile : IInputUpdater
    {
        public event Action<Vector2> Pressed;
        public event Action<Vector2> Move;
        public event Action<Vector2> Released;
        
        public void Update() { }
    }
}
