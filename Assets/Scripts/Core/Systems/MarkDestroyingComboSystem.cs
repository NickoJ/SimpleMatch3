using Klyukay.SimpleMatch3.Core.Components;
using Klyukay.SimpleMatch3.Core.Events;
using Leopotam.Ecs;
using Klyukay.SimpleMatch3.Core.Utils;

using static Klyukay.SimpleMatch3.Core.Utils.FieldUtils;

namespace Klyukay.SimpleMatch3.Core.Systems
{
    internal class MarkDestroyingComboSystem : BaseTickSystem
    {
        
        private readonly EcsWorld _world;

        public MarkDestroyingComboSystem(EcsWorld world, Match3State state) : base(state)
        {
            _world = world;
        }

        protected override void ExecuteTick()
        {
            foreach (var stone in SearchStonesInCombo(State.StoneField))
            {
                _world.EnsureComponent<Destroyed>(stone.eid);
            }
        }
        
    }
    
}