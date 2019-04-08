using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core.Utils
{
    public static class ArrayUtils
    {
        public static bool InRange<T>(this T[,] field, int2 pos)
        {
            for (int i = 0; i < 2; ++i)
            {
                if (pos[i] < 0 || pos[i] >= field.GetLength(i)) return false;
            }

            return true;
        }

        public static T Get<T>(this T[,] field, int2 pos) => field[pos.x, pos.y];
        public static void Set<T>(this T[,] field, int2 pos, T value) => field[pos.x, pos.y] = value;

        public static void Deconstruct<T>(this T[,] field, out int w, out int h)
        {
            w = field.GetLength(0);
            h = field.GetLength(1);
        }

        public static bool IsNeighboringPositions(int2 lhv, int2 rhv)
        {
            var delta = rhv - lhv;
            delta = math.abs(delta);
            
            return (delta.x == 1 && delta.y == 0) || (delta.y == 1 && delta.x == 0);
        }
        
    }
    
}