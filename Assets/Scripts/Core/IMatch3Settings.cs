using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core
{
    
    public interface IMatch3Settings
    {

        int2 Size { get; }
        int ColorsCount { get; }
    }
    
}