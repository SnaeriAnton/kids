using UnityEngine;

namespace Game
{
    public readonly struct GameConfigData
    {
        public readonly Sprite Sprite;
        public readonly string ID;
        
        public GameConfigData(Sprite sprite, string id)
        {
            Sprite = sprite;
            ID = id;
        }
    }
}
