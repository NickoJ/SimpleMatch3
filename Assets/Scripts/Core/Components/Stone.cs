using Unity.Mathematics;
// ReSharper disable InconsistentNaming

namespace Klyukay.SimpleMatch3.Core.Components
{
    
    public class Stone
    {
        internal int eid;
        internal int2 position;
        internal Color color;

        public int Eid => eid;

        public int2 Position => position;
        
        public Color Color => color;
    }
    
}