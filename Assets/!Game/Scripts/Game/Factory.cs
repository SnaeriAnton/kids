using UnityEngine;
using Zenject;

namespace Game
{
    public class Factory
    {
        private readonly Cube _cubeTemplate;
        private readonly DiContainer _container;

        public Factory(Cube cubeTemplate, DiContainer container)
        {
            _cubeTemplate = cubeTemplate;
            _container = container;
        }

        public Cube Create(GameConfigData data, Vector3 position)
        {
            Cube cube = _container.InstantiatePrefabForComponent<Cube>(
                _cubeTemplate,
                position,
                Quaternion.identity,
                parentTransform: null);
            cube.Construct(data, position);
            return cube;
        }
    }
}