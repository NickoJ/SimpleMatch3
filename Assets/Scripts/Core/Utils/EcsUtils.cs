using Klyukay.SimpleMatch3.Core.Components;
using Leopotam.Ecs;
using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core.Utils
{
    
    internal static class EcsUtils
    {

        private static bool cap;
        
        internal static T EnsureComponent<T>(this EcsWorld world, int eid) where T : class, new()
            => world.EnsureComponent<T>(eid, out cap);
        
        internal static void CreateStone(EcsWorld world, Match3State state, ref Stone stone, int2 pos)
        {
            if (stone == null)
            {
                var eid = world.CreateEntityWith(out stone);
                stone.eid = eid;
            }

            stone.color = state.Randomizer.RandomColor;
            stone.position = pos;
        }
        
    }
    
}