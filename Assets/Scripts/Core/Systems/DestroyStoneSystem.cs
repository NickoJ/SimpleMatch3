using Klyukay.SimpleMatch3.Core.Components;
using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Core.Utils;
using Leopotam.Ecs;

namespace Klyukay.SimpleMatch3.Core.Systems
{
    internal class DestroyStoneSystem : BaseTickSystem
    {

        private readonly EcsWorld _world;
        private readonly ICoreEventsReceiver _eventsReceiver;
        private readonly EcsFilter<Stone, Destroyed> _destroyed;
        
        public DestroyStoneSystem(EcsWorld world, ICoreEventsReceiver eventsReceiver, Match3State state) : base(state)
        {
            _world = world;
            _eventsReceiver = eventsReceiver;
            _destroyed = world.GetFilter<EcsFilter<Stone, Destroyed>>();
        }

        protected override void ExecuteTick()
        {
            var field = State.StoneField;
            foreach (var index in _destroyed)
            {
                var stone = _destroyed.Components1[index];
                field.Set(stone.position, null);
                _eventsReceiver.StoneDestroyed(new StoneDestroyEvent(stone));
                _world.RemoveEntity(stone.eid);
            }

            if (_destroyed.EntitiesCount > 0) State.TickProcessed = true;
        }
        
    }
}