using Klyukay.SimpleMatch3.Core.Components;
using Unity.Mathematics;
// ReSharper disable InconsistentNaming

namespace Klyukay.SimpleMatch3.Core.Events
{
    
    public readonly struct StoneMoveEvent
    {
        public readonly int id;
        public readonly int2 pos;

        internal StoneMoveEvent(Stone stone)
        {
            if (stone == null)
            {
                this = default;
                return;
            }

            id = stone.eid;
            pos = stone.position;
        }
    }
    
}