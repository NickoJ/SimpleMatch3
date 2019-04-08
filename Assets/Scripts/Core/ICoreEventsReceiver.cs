using Klyukay.SimpleMatch3.Core.Events;

namespace Klyukay.SimpleMatch3.Core
{
    
    public interface ICoreEventsReceiver
    {

        void StoneCreated(in StoneCreateEvent e);
        void StoneDestroyed(in StoneDestroyEvent e);
        void StoneMoved(in StoneMoveEvent e);
        void StoneChangeColor(in StoneChangeColorEvent e);
    }
    
}