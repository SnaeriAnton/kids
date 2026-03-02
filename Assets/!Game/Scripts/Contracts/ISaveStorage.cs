using Game.SaveSystem;

namespace Game.Contracts
{
    public interface ISaveStorage
    {
        public PlayerData Data { get; }
        public void Save();
    }
}
