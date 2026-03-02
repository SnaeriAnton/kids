using UnityEngine;
using Newtonsoft.Json;
using Zenject;
using Game.Contracts;

namespace Game.SaveSystem
{
    public class SaveManager : ISaveStorage, IInitializable
    {
        private const string KEY_SAVE = "KEY_SAVE";

        public PlayerData Data { get; private set; }
        
        public void Initialize() =>
            Load();

        public void Load()
        {
            if (PlayerPrefs.HasKey(KEY_SAVE))
                Data = JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString(KEY_SAVE));
            else
                Data = new();
        }

        public void Save() => PlayerPrefs.SetString(KEY_SAVE, JsonConvert.SerializeObject(Data));
    }
}