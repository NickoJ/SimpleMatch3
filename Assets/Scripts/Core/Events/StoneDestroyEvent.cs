using Klyukay.SimpleMatch3.Core.Components;
// ReSharper disable InconsistentNaming

namespace Klyukay.SimpleMatch3.Core.Events
{
    public readonly struct StoneDestroyEvent
    {

        public readonly int id;
        
        internal StoneDestroyEvent(Stone stone)
        {
            if (stone == null)
            {
                this = default;
                return;
            }

            id = stone.eid;
        }
    }
}