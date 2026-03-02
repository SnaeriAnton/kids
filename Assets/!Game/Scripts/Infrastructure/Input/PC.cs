using System;
using UnityEngine;

namespace Game.InputSystem
{
    public class PC : IInputUpdater
    {
        public event Action<Vector2> Pressed;
        public event Action<Vector2> Move;
        public event Action<Vector2> Released;
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0)) Pressed?.Invoke(Input.mousePosition);
            if (Input.GetMouseButton(0)) Move?.Invoke(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) Released?.Invoke(Input.mousePosition);
        }
    }
}
