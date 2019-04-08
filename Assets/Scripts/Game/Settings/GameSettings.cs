using Klyukay.SimpleMatch3.Core;
using Klyukay.SimpleMatch3.Game.Field;
using Unity.Mathematics;
using UnityEngine;

using static Klyukay.SimpleMatch3.Core.ColorUtils;

// ReSharper disable ConvertToAutoProperty

namespace Klyukay.SimpleMatch3.Game.Settings
{

    [CreateAssetMenu(menuName = "Match3/Settings", fileName = "Match3Settings")]
    public class GameSettings : ScriptableObject, IMatch3Settings
    {

        private const int MinFieldSize = 3;

        [SerializeField] private int2 size = MinFieldSize;

        [SerializeField, Range(0.1f, 2f)] private float timeBetweenTicks = 0.1f;

        [SerializeField, Range(MinColorsCount, MaxColorsCount)]
        private int colorsCount = MinColorsCount;

        [SerializeField] private StoneController stonePrefab;
        
        [SerializeField] private StoneSprites stoneSprites;

        public int2 Size
        {
            get
            {
                var validSize = size >= MinFieldSize;
                if (!validSize.x || !validSize.y) size = MinFieldSize;

                return size;
            }
        }
        
        public float TimeBetweenTicks => timeBetweenTicks;
        
        public int ColorsCount => colorsCount;

        public StoneController StonePrefab => stonePrefab;
        
        public StoneSprites StoneSprites => stoneSprites;
    }

}