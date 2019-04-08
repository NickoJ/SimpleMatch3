using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Klyukay.SimpleMatch3.Core.Components;
using Unity.Mathematics;

namespace Klyukay.SimpleMatch3.Core.Utils
{
    
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    internal static class FieldUtils
    {

        internal static bool Swap(this Stone[,] field, int2 lPos, int2 rPos)
        {
            if (field == null) return false;
            if (!field.InRange(lPos) || !field.InRange(rPos)) return false;

            var ls = field.Get(lPos);
            var rs = field.Get(rPos);

            if (ls != null) ls.position = rPos;
            if (rs != null) rs.position = lPos;

            field.Set(rPos, ls);
            field.Set(lPos, rs);

            return true;
        }

        internal static bool HasActiveCombo(Stone[,] field) =>
            SearchStonesInCombo(field).GetEnumerator().MoveNext();   

        internal static IEnumerable<Stone> SearchStonesInCombo(Stone[,] field)
        {
            var (w, h) = field;
            
            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (field[x, y] == null) continue;
                    
                    var c = field[x, y].color;
                    
                    if (x < field.GetLength(0) - 2)
                    {
                        var s1 = field[x + 1, y];
                        var s2 = field[x + 2, y];
                        var hasCombo = s1 != null && s2 != null;
                        hasCombo = hasCombo && c == s1.color && c == s2.color;
                        if (hasCombo)
                        {
                            foreach (var s in GetAllWithColor(field, c, new int2(x, y), new int2(1, 0)))
                            {
                                yield return s;
                            }
                        }
                    }

                    if (y < field.GetLength(1) - 2)
                    {
                        var s1 = field[x, y + 1];
                        var s2 = field[x, y + 2];
                        var hasCombo = s1 != null && s2 != null;
                        hasCombo = hasCombo && c == s1.color && c == s2.color;
                        if (hasCombo)
                        {
                            foreach (var s in GetAllWithColor(field, c, new int2(x, y), new int2(0, 1)))
                            {
                                yield return s;
                            }
                        }
                    }
                }
            }
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

        private static IEnumerable<Stone> GetAllWithColor(Stone[,] field, Color color, 
            int2 startPos, int2 offset)
        {
            for (var pos = startPos; field.InRange(pos); pos += offset)
            {
                var stone = field.Get(pos);
                
                if (stone == null) yield break;
                if (stone.color != color) yield break;

                yield return stone;
            }
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