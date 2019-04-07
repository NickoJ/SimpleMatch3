using Klyukay.SimpleMatch3.Core;
using UnityEngine;

namespace Klyukay.SimpleMatch3.Game
{

    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameSettings settings;
        
        private Match3Core _core;
        
        private void OnEnable()
        {
            _core = new Match3Core(settings);
            _core.Initialize();
        }

        private void OnDisable()
        {
            _core?.Dispose();
            _core = null;
        }
    }

}