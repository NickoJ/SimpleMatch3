using Klyukay.SimpleMatch3.Core.Components;

namespace Klyukay.SimpleMatch3.Core
{
    
    internal class Match3State
    {

        internal readonly Randomizer Randomizer; 
        internal readonly Stone[,] StoneField;

        public Match3State(IMatch3Settings settings)
        {
            Randomizer = new Randomizer(settings.ColorsCount);
            StoneField = new Stone[settings.Size.x, settings.Size.y];
        }
        
    }
    
}