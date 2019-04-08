using Klyukay.SimpleMatch3.Core.Components;
using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Core.Utils;
using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core.Systems
{
    internal class FallStoneSystem : BaseTickSystem
    {

        private readonly ICoreEventsReceiver _eventsReceiver;
        
        public FallStoneSystem(ICoreEventsReceiver eventsReceiver, Match3State state) : base(state)
        {
            _eventsReceiver = eventsReceiver;
        }

        protected override void ExecuteTick()
        {
            var field = State.StoneField;
            var (w, h) = field;

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (field[x, y] != null) continue;

                    var pos = new int2(x, y);
                    var above = FindStoneAbove(field, pos);
                    if (above == null) break;

                    field.Swap(pos, above.position);

                    State.TickProcessed = true;
                    _eventsReceiver.StoneMoved(new StoneMoveEvent(above));
                }
            }
        }

        private static Stone FindStoneAbove(Stone[,] field, int2 pos)
        {
            var (_, h) = field;

            for (int y = pos.y + 1; y < h; ++y)
            {
                if (field[pos.x, y] == null) continue;
                return field[pos.x, y];
            }

            return null;
        }
        
    }
    
}