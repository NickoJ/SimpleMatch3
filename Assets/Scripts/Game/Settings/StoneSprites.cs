using UnityEngine;
using Color = Klyukay.SimpleMatch3.Core.Color;

namespace Klyukay.SimpleMatch3.Game.Settings
{

    [CreateAssetMenu(menuName = "Match3/StoneSprites", fileName = "StoneSprites")]
    public class StoneSprites : ScriptableObject
    {

        [SerializeField] private StoneData[] stones;

        public Sprite this[Color color]
        {
            get
            {
                if (stones == null) return null;

                foreach (var stone in stones)
                {
                    if (stone.Color == color) return stone.Sprite;
                }

                return null;
            }
        }
        
        [System.Serializable]
        private struct StoneData
        {
            [SerializeField] private Color color;
            [SerializeField] private Sprite sprite;

            public Color Color => color;
            public Sprite Sprite => sprite;

        }
        
    }

}