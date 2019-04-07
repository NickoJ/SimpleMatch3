using Klyukay.SimpleMatch3.Core.Components;
using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Core.Utils;
using Leopotam.Ecs;
using Unity.Mathematics;
using static Klyukay.SimpleMatch3.Core.Utils.FieldUtils;

namespace Klyukay.SimpleMatch3.Core.Systems
{

    internal class FieldInitializeSystem : IEcsInitSystem
    {
        
        private readonly EcsWorld _world;
        private readonly Match3State _state;
        private readonly ICoreEventsReceiver _eventsReceiver;

        public FieldInitializeSystem(EcsWorld world, Match3State state, ICoreEventsReceiver eventsReceiver)
        {
            _world = world;
            _state = state;
            _eventsReceiver = eventsReceiver;
        }

        public void Initialize()
        {
            var (w, h) = _state.StoneField;
            do
            {
                for (int x = 0; x < w; ++x)
                {
                    for (int y = 0; y < h; ++y)
                    {
                        CreateStone(ref _state.StoneField[x,y], new int2(x, y));
                    }
                }
                
            } while (HasActiveCombo(_state.StoneField) || !HasAvailableCombo(_state.StoneField));

            // Notify about new stones
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    var stone = _state.StoneField[x, y];
                    stone.valid = true;
                    
                    _eventsReceiver.StoneCreated(new StoneCreateEvent(stone.eid, new int2(x, y), stone.color));
                }
            }
        }

        public void Destroy() {}

        private void CreateStone(ref Stone stone, int2 pos)
        {
            if (stone == null)
            {
                var eid = _world.CreateEntityWith(out stone);
                stone.eid = eid;
            }

            stone.color = _state.Randomizer.RandomColor;
            stone.position = pos;
            stone.valid = false;
        }
        
    }

}