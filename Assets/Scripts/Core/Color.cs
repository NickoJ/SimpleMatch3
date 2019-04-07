namespace Klyukay.SimpleMatch3.Core
{
    
    public enum Color : byte
    {
        Red,
        Green,
        Blue,
        Yellow,
        Purple
    }
    
    public static class ColorUtils
    {

        public const int MinColorsCount = 2;
        public const int MaxColorsCount = (int) Color.Purple + 1;

    }
    
}