using Klyukay.SimpleMatch3.Core.Components;
// ReSharper disable InconsistentNaming

namespace Klyukay.SimpleMatch3.Core.Events
{
    public readonly struct StoneChangeColorEvent
    {
        public readonly int id;
        public readonly Color color;

        internal StoneChangeColorEvent(Stone stone)
        {
            if (stone == null)
            {
                this = default;
                return;
            }

            id = stone.eid;
            color = stone.color;
        }
    }
}