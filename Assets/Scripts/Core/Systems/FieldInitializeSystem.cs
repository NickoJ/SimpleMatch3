using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Core.Utils;
using Leopotam.Ecs;
using Unity.Mathematics;
using static Klyukay.SimpleMatch3.Core.Utils.FieldUtils;
using static Klyukay.SimpleMatch3.Core.Utils.EcsUtils;

namespace Klyukay.SimpleMatch3.Core.Systems
{

    internal class FieldInitializeSystem : BaseTickSystem, IEcsInitSystem
    {
        
        private readonly EcsWorld _world;
        private readonly ICoreEventsReceiver _eventsReceiver;

        public FieldInitializeSystem(EcsWorld world, ICoreEventsReceiver eventsReceiver, Match3State state) : base(state)
        {
            _world = world;
            _eventsReceiver = eventsReceiver;
        }

        public void Initialize()
        {
            GenerateField();

            foreach (var stone in State.StoneField)
            {
                _eventsReceiver.StoneCreated(new StoneCreateEvent(stone));
            }
        }

        protected override void ExecuteTick()
        {
            if (HasAvailableCombo(State.StoneField)) return;
            
            GenerateField();

            State.TickProcessed = true;
            
            foreach (var stone in State.StoneField)
            {
                _eventsReceiver.StoneChangeColor(new StoneChangeColorEvent(stone));
            }
        }

        private void GenerateField()
        {
            var (w, h) = State.StoneField;
            do
            {
                for (int x = 0; x < w; ++x)
                {
                    for (int y = 0; y < h; ++y)
                    {
                        CreateStone(_world, State, ref State.StoneField[x,y], new int2(x, y));
                    }
                }
            } while (HasActiveCombo(State.StoneField) || !HasAvailableCombo(State.StoneField));
        }

        public void Destroy() {}
        
    }

}