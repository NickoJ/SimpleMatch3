using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Core.Utils;
using Leopotam.Ecs;
using Unity.Mathematics;
using static Klyukay.SimpleMatch3.Core.Utils.EcsUtils;

namespace Klyukay.SimpleMatch3.Core.Systems
{
    
    internal class CreateNewStonesSystem : BaseTickSystem
    {

        private readonly EcsWorld _world;
        private readonly ICoreEventsReceiver _eventsReceiver;
        
        public CreateNewStonesSystem(EcsWorld world, ICoreEventsReceiver eventsReceiver, Match3State state) : base(state)
        {
            _world = world;
            _eventsReceiver = eventsReceiver;
        }

        protected override void ExecuteTick()
        {
            var field = State.StoneField;
            var (w, h) = field;
            var y = h - 1;

            for (int x = 0; x < w; ++x)
            {
                if (field[x, y] != null) continue;

                CreateStone(_world, State, ref field[x,y], new int2(x, y));
                _eventsReceiver.StoneCreated(new StoneCreateEvent(field[x, y]));
                
                State.TickProcessed = true;
            }
        }
    }
    
}