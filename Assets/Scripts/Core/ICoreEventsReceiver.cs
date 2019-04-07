using Klyukay.SimpleMatch3.Core.Events;
using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core
{
    
    public interface ICoreEventsReceiver
    {

        void StoneCreated(in StoneCreateEvent e);
        void StoneDestroyed(int id);
        void StoneMoved(int id, int2 pos);

    }
    
}