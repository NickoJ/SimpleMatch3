using System.Diagnostics.CodeAnalysis;
using Klyukay.SimpleMatch3.Core.Components;
using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core.Utils
{
    
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    internal static class FieldUtils
    {

        internal static bool HasActiveCombo(Stone[,] field)
        {
            var (w, h) = field;
            
            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    var c = field[x, y].color;
                    var hasCombo = false;
                    
                    if (x < field.GetLength(0) - 2)
                    {
                        hasCombo = c == field[x + 1, y].color && c == field[x + 2, y].color;
                    }

                    if (y < field.GetLength(1) - 2)
                    {
                        hasCombo = hasCombo || c == field[x, y + 1].color && c == field[x, y + 2].color;
                    }

                    if (hasCombo) return true;
                }
            }

            return false;
        }

        internal static bool HasAvailableCombo(Stone[,] field)
        {
            var (w, h) = field;

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (HasAvailableCombo(new int2(x, y), field)) return true;
                }
            }

            return false;
        }

        private static bool HasAvailableCombo(int2 pos, Stone[,] field)
        {
            foreach (var combo in AllCombos)
            {
                if (combo.HasCombo(pos, field)) return true;
            }

            return false;
        }

        private static readonly ComboCheck[] AllCombos = 
        {
            // . *
            // . *
            // > .
            new ComboCheck(new int2(1, 0), new int2(1, 1), new int2(1, 2)),
            
            // * .
            // * .
            // . <
            new ComboCheck(new int2(-1, 0), new int2(-1, 1), new int2(-1, 2)),
            
            // *
            // *
            // .
            // ^
            new ComboCheck(new int2(0, 1), new int2(0, 2), new int2(0, 3)),
            
            // v
            // .
            // *
            // *
            new ComboCheck(new int2(0, -1), new int2(0, -2), new int2(0, -3)),
            
            // . <
            // * .
            // * .
            new ComboCheck(new int2(-1, 0), new int2(-1, -1), new int2(-1, -2)),
            
            // > .
            // . *
            // . *
            new ComboCheck(new int2(1, 0), new int2(1, -1), new int2(1, -2)),
            
            // . *
            // > .
            // . *
            new ComboCheck(new int2(1, 0), new int2(1, 1), new int2(1, -1)),
            
            // * .
            // . <
            // * .
            new ComboCheck(new int2(-1, 0), new int2(-1, 1), new int2(-1, -1)),
            
            // * * . <
            new ComboCheck(new int2(-1, 0), new int2(-2, 0), new int2(-3, 0)),
            
            // > . * *
            new ComboCheck(new int2(1, 0), new int2(2, 0), new int2(3, 0)),
            
            // . v .
            // * . *
            new ComboCheck(new int2(0, -1), new int2(-1, -1), new int2(1, -1)),
            
            // * . *
            // . ^ .
            new ComboCheck(new int2(0, 1), new int2(-1, 1), new int2(1, 1)),
            
            // v . .
            // . * *
            new ComboCheck(new int2(0, -1), new int2(1, -1), new int2(2, -1)),
            
            // . * *
            // ^ . .
            new ComboCheck(new int2(0, 1), new int2(1, 1), new int2(2, 1)),
            
            // * * .
            // . . ^
            new ComboCheck(new int2(0, 1), new int2(-1, 1), new int2(-2, 1)),
            
            // . . v
            // * * .
            new ComboCheck(new int2(0, -1), new int2(-1, -1), new int2(-2, -1))
        };
        
        private readonly struct ComboCheck
        {

            private readonly int2 _movePos;
            private readonly int2 _otherPos1;
            private readonly int2 _otherPos2;

            public ComboCheck(int2 movePos, int2 otherPos1, int2 otherPos2)
            {
                _movePos = movePos;
                _otherPos1 = otherPos1;
                _otherPos2 = otherPos2;
            }

            [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
            public bool HasCombo(int2 pos, Stone[,] field)
            {
                var mp = pos + _movePos;
                var op1 = pos + _otherPos1;
                var op2 = pos + _otherPos2;

                if (!field.InRange(pos)) return false;
                if (!field.InRange(mp)) return false;
                if (!field.InRange(op1)) return false;
                if (!field.InRange(op2)) return false;

                var color = field.Get(pos).color;
                
                if (color == field.Get(mp).color) return false;
                if (color != field.Get(op1).color) return false;
                if (color != field.Get(op2).color) return false;

                return true;
            }
            
        }
        
    }
    
}