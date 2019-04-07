using Klyukay.SimpleMatch3.Core;
using Klyukay.SimpleMatch3.Game.Field;
using Klyukay.SimpleMatch3.Game.Settings;
using UnityEngine;

namespace Klyukay.SimpleMatch3.Game
{

    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameSettings settings;
        [SerializeField] private FieldController field;
        
        private Match3Core _core;
        private bool _destroyed;
        
        private void Awake()
        {
            field.Initialize(settings);
        }

        private void OnEnable()
        {
            _core = new Match3Core(field, settings);
            
            _core.Initialize();
        }

        private void OnDisable()
        {
            if (!_destroyed) Reset();
        }

        private void Reset()
        {
            field.Reset();
            
            _core?.Dispose();
            _core = null;
        }
    }

}