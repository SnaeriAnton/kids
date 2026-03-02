using System.Collections.Generic;
using System.Linq;
using Zenject;
using Game.Contracts;
using UnityEngine;

namespace Game
{
    public class SaveLoader : IInitializable
    {
        private readonly Factory _factory;
        private readonly Tower _tower;
        private readonly ISaveStorage _save;
        private readonly IGameConfigProvider _cubeData;

        public SaveLoader(Tower tower, Factory factory, ISaveStorage save, IGameConfigProvider cubeData)
        {
            _tower = tower;
            _factory = factory;
            _save = save;
            _cubeData = cubeData;
        }
        
        public void Initialize()
        {
            if (_save.Data.Cubes == null) return;
            
            List<GameConfigData> configs = new(_cubeData.GetConfigs());
            
            foreach (SaveSystem.CubeData data in _save.Data.Cubes)
            {
                GameConfigData confData = configs.First(c => c.ID == data.CubeID);
                Cube cube = _factory.Create(confData, data.Position.AsVec());
                _tower.AddCube(cube);
            }
        }
    }
}