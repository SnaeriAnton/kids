using UnityEngine;

namespace Game
{
    public class CameraBounds
    {
        private readonly Camera _camera;

        public CameraBounds(Camera camera)
        {
            _camera = camera;
        }
        
        public bool CheckScreenBoard(Bounds bounds)
        {
            Vector3 camPos = _camera.transform.position; 
            float camHeight = _camera.orthographicSize;
            float camWidth = camHeight * _camera.aspect;

            float left   = camPos.x - camWidth;
            float right  = camPos.x + camWidth;
            float bottom = camPos.y - camHeight;
            float top    = camPos.y + camHeight;
            
            return bounds.min.x < left || bounds.max.x > right ||
                   bounds.min.y < bottom || bounds.max.y > top;
        }
    }
}
