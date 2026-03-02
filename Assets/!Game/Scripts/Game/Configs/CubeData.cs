using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Contracts;

namespace Game
{
    [CreateAssetMenu(fileName = "CubeData", menuName = "Kids/Cube data")]
    public class CubeData : ScriptableObject, IGameConfigProvider
    {
        [SerializeField] private List<CubeInfo> _cubeInfos;
        
        public IReadOnlyList<GameConfigData> GetConfigs()
        {
            List<GameConfigData> configs = new();
            
            foreach (CubeInfo info in _cubeInfos)
                configs.Add(new (info.Sprite, info.ID));
            
            return configs;
        }
    }

    [Serializable]
    public struct CubeInfo
    {
        public Sprite Sprite;
        public string ID;
    }
}