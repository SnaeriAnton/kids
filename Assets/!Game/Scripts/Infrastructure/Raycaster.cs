using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Raycast
{
    public class Raycaster
    {
        private const float DISTANCE = 100f;
        
        private readonly Camera _camera;
        private readonly LayerMask _layerMask;
        private readonly EventSystem _eventSystem;

        public Raycaster(Camera camera, EventSystem eventSystem, LayerMask layerMask)
        {
            _camera = camera;
            _eventSystem = eventSystem;
            _layerMask = layerMask;
        }

        public bool TryGetComponent<T>(Vector3 position, out T component) where T : Component
        {
            component = null;
            Ray ray = _camera.ScreenPointToRay(position);
            RaycastHit2D[] hit = Physics2D.GetRayIntersectionAll(ray, DISTANCE, _layerMask);

            if (hit.Length > 0)
                foreach (RaycastHit2D h in hit)
                    if (h.transform.gameObject.TryGetComponent(out component))
                        return component;

            return component;
        }

        public bool TryGetUIComponent<T>(Vector2 mousePosition, out T component) where T : Component
        {
            component = null;
            PointerEventData eventData = new(_eventSystem) { position = mousePosition };
            List<RaycastResult> results = new();
            _eventSystem.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
                if (result.gameObject.TryGetComponent(out component))
                    return component;

            return component;
        }
    }
}