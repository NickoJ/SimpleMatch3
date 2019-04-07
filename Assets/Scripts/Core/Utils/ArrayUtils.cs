using Klyukay.SimpleMatch3.Core.Components;
using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core.Utils
{
    
    internal static class ArrayUtils
    {

        internal static bool InRange<T>(this T[,] field, int2 pos)
        {
            for (int i = 0; i < 2; ++i)
            {
                if (pos[i] < 0 || pos[i] >= field.GetLength(i)) return false;
            }

            return true;
        }

        internal static T Get<T>(this T[,] field, int2 pos) => field[pos.x, pos.y];
        
        internal static void Deconstruct(this Stone[,] field, out int w, out int h)
        {
            w = field.GetLength(0);
            h = field.GetLength(1);
        }
        
    }
    
}