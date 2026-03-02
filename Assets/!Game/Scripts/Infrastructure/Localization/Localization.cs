using System.Collections.Generic;
using Game.Contracts;

namespace Game.LocalizationSystem
{
    public class Localization : ILocalization
    {
        private readonly Dictionary _dictionary;
        private readonly  LanguageCodes _language;
        
        public Localization(Dictionary dictionary, LanguageCodes code)
        {
            _dictionary = dictionary;
            _language = code;
            _dictionary.Init();
        }

        public string GetTranslate(string key)
        {
            WordData data = _dictionary.GetTranslate(key);

            switch (_language)
            {
                case LanguageCodes.ru:
                    return data.RU;
                case LanguageCodes.en:
                    return data.EN;
            }

            throw new KeyNotFoundException();
        }
    }
}