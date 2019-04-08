using Leopotam.Ecs;

namespace Klyukay.SimpleMatch3.Core.Events
{
    
    internal abstract class BaseTickSystem : IEcsRunSystem
    {

        protected readonly Match3State State;
        
        protected BaseTickSystem(Match3State state)
        {
            State = state;
        }
        
        public void Run()
        {
            if (!State.TickProcessed) ExecuteTick();
        }

        protected abstract void ExecuteTick();

    }
    
}