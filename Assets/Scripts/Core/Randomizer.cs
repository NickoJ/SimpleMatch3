using System;

namespace Klyukay.SimpleMatch3.Core
{
    
    internal class Randomizer
    {

        private Random _random;
        private int _colorsCount;
        
        internal Randomizer(int colorsCount)
        {
            _random = new Random();
            _colorsCount = colorsCount;
        }

        internal Color RandomColor => (Color)_random.Next(0, _colorsCount);

    }
    
}