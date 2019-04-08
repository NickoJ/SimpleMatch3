using System.Collections;
using Klyukay.SimpleMatch3.Core;
using Klyukay.SimpleMatch3.Game.Field;
using Klyukay.SimpleMatch3.Game.Settings;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.SimpleMatch3.Game
{

    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameSettings settings;
        [SerializeField] private FieldController field;

        [SerializeField] private Button restartButton;
        
        private Match3Core _core;

        public bool IsBusy { get; private set; }

        private void Awake()
        {
            field.Initialize(this, settings);
        }

        private void Start()
        {
            restartButton.onClick.AddListener(Restart);
            
            InitializeGame();
        }

        public void Restart()
        {
            Reset();
            InitializeGame();
        }
        
        private void InitializeGame()
        {
            _core = new Match3Core(field, settings);
            _core.Initialize();
        }

        private void Reset()
        {
            StopAllCoroutines();
            IsBusy = false;
            
            field.Reset();
            
            _core?.Dispose();
            _core = null;
        }

        public void SwapStones(int2 lhvPos, int2 rhvPos)
        {
            if (IsBusy || _core.IsBusy) return;

            IsBusy = true;
            _core.Swap(lhvPos, rhvPos);
            StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            var waitTime = new WaitForSeconds(settings.TimeBetweenTicks);
            do
            {
                _core.Tick();
                yield return waitTime;
            } while (_core.IsBusy);

            IsBusy = false;
        }
        
    }

}