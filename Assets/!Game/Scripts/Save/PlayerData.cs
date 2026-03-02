using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public List<CubeData> Cubes;
    }
    
    [Serializable]
    public class CubeData
    {
        public string CubeID;
        public JsonVector3 Position;
    }
    
    [Serializable]
    public struct JsonVector3
    {
        public float X;
        public float Y;
        public float Z;

        public static implicit operator JsonVector3(Vector3 vec)
        {
            return new JsonVector3()
            {
                X = vec.x,
                Y = vec.y,
                Z = vec.z
            };
        }

        public readonly Vector3 AsVec() { return new Vector3(X, Y, Z); }
    }
}