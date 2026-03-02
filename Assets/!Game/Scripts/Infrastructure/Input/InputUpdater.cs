using Zenject;
using Game.Contracts;

namespace Game.InputSystem
{
    public class InputUpdater : ITickable  
    {
        private readonly IInputUpdater _input;
        
        public InputUpdater(IInput input) => _input = input as IInputUpdater;
        
        public void Tick() => _input?.Update();
    }
}
