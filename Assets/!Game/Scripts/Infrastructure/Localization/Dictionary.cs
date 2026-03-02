using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.LocalizationSystem
{
    [CreateAssetMenu(fileName = "Dictionary", menuName = "Localization/Dictionary")]
    public class Dictionary : ScriptableObject
    {
        [SerializeField] private List<WordData> _words;

        private Dictionary<string, WordData> _wordDictionary = new();

        public void Init()
        {
            foreach (WordData word in _words)
                _wordDictionary[word.Key] = word;
        }

        public WordData GetTranslate(string key) => _wordDictionary[key];
        
    }

    [Serializable]
    public struct WordData
    {
        public string Key;
        public string RU;
        public string EN;
    }
}