using UnityEngine;

namespace Game.Contracts
{
    public interface ILocalization
    {
        public string GetTranslate(string key);
    }
}
