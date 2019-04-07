using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core.Events
{
    
    public readonly struct StoneCreateEvent
    {
        
        public readonly int id;
        public readonly int2 pos;
        public readonly Color color;

        internal StoneCreateEvent(int id, int2 pos, Color color)
        {
            this.id = id;
            this.pos = pos;
            this.color = color;
        }
        
    }
    
}