using Klyukay.SimpleMatch3.Core;
using Unity.Mathematics;
using UnityEngine;

using static Klyukay.SimpleMatch3.Core.ColorUtils;

// ReSharper disable ConvertToAutoProperty

namespace Klyukay.SimpleMatch3.Game
{

    [CreateAssetMenu(menuName = "Match3/Settings", fileName = "Match3Settings")]
    public class GameSettings : ScriptableObject, IMatch3Settings
    {

        private const int MinFieldSize = 3;

        [SerializeField] private int2 size = MinFieldSize;

        [SerializeField, Range(MinColorsCount, MaxColorsCount)]
        private int colorsCount = MinColorsCount;

        public int2 Size
        {
            get
            {
                var validSize = size >= MinFieldSize;
                if (!validSize.x || !validSize.y) size = MinFieldSize;

                return size;
            }
        }
        public int ColorsCount => colorsCount;
        
    }

}