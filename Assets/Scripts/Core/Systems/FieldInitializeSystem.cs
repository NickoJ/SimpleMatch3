using Klyukay.SimpleMatch3.Core.Components;
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

        public FieldInitializeSystem(EcsWorld world, Match3State state)
        {
            _world = world;
            _state = state;
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