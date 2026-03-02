using System.Collections.Generic;

namespace Game.Contracts
{
    public interface IGameConfigProvider
    {
        public IReadOnlyList<GameConfigData> GetConfigs();
    }
}