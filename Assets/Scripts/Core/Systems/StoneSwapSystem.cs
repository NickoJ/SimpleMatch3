using Klyukay.SimpleMatch3.Core.Components;
using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Core.Utils;
using Leopotam.Ecs;

using static Klyukay.SimpleMatch3.Core.Utils.FieldUtils;

namespace Klyukay.SimpleMatch3.Core.Systems
{
    internal class StoneSwapSystem : BaseTickSystem
    {

        private readonly EcsWorld _world;
        private readonly ICoreEventsReceiver _eventsReceiver;

        private readonly EcsFilter<Stone, Swapping> _swappingStones;
        
        public StoneSwapSystem(EcsWorld world, ICoreEventsReceiver eventsReceiver, Match3State state) : base(state)
        {
            _world = world;
            _eventsReceiver = eventsReceiver;
            _swappingStones = world.GetFilter<EcsFilter<Stone, Swapping>>();
        }

        protected override void ExecuteTick()
        {
            if (_swappingStones.EntitiesCount < 2) return;

            var s1 = _swappingStones.Components1[0];
            var s2 = _swappingStones.Components1[1];
            if (s1 == null || s2 == null) return;

            var field = State.StoneField;
            
            if (!field.Swap(s1.position, s2.position)) return;

            if (HasActiveCombo(field))
            {
                State.TickProcessed = true;
                _eventsReceiver.StoneMoved(new StoneMoveEvent(s1));
                _eventsReceiver.StoneMoved(new StoneMoveEvent(s2));
            }
            else
            {
                field.Swap(s1.position, s2.position);
            }
        }
        
    }
    
}