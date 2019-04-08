using System.Collections.Generic;
using Klyukay.SimpleMatch3.Core;
using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Core.Systems;
using Klyukay.SimpleMatch3.Game.Helpers;
using Klyukay.SimpleMatch3.Game.Settings;
using Unity.Mathematics;
using UnityEngine;

using static Klyukay.SimpleMatch3.Core.Utils.ArrayUtils;

namespace Klyukay.SimpleMatch3.Game.Field
{

    public class FieldController : MonoBehaviour, ICoreEventsReceiver
    {

        [SerializeField] private Camera cam;

        private GameManager _game;        
        private MonoPool<StoneController> _stonePool;
        private StoneSprites _stoneSprites;

        private bool _isTouchDown;
        private StoneController _selectedStone;
        
        private readonly Dictionary<int, StoneController> _stonesById = new Dictionary<int, StoneController>();
        
        public void Initialize(GameManager game, GameSettings settings)
        {
            _game = game;
            _stoneSprites = settings.StoneSprites;

            var size = settings.Size;
            _stonePool = new MonoPool<StoneController>(settings.StonePrefab, size.x * size.y);

            SetupCamera(size);
        }

        public void Reset()
        {
            foreach (var stone in _stonesById.Values)
            {
                stone.Reset();
                _stonePool.Release(stone);
            }
            _stonesById.Clear();
        }
        
        private void SetupCamera(int2 size)
        {
            float orthographic;
            if (size.y >= size.x) orthographic = (size.y + 1) / 2f;
            else orthographic = (size.x + 1) / cam.aspect / 2f;
            cam.orthographicSize = orthographic;
            
            var cT = cam.transform;
            cT.position = new Vector3((size.x - 1) / 2f, (size.y - 1) / 2f, cT.position.z);
        }

        private void OnStoneTouchEvent(TouchEvent e)
        {
            switch (e.State)
            {
                case TouchState.Down: ProcessTouchDown(e.Controller); break;
                case TouchState.Up: ProcessTouchUp(e.Controller); break;
                case TouchState.Enter: ProcessTouchEnter(e.Controller); break;
            }
        }

        private void ProcessTouchDown(StoneController s)
        {
            _isTouchDown = true;
            if (_selectedStone == null || !IsNeighboringPositions(_selectedStone.Pos, s.Pos))
            {
                _selectedStone = s;
            }
            else
            {
                var f = _selectedStone;
                _selectedStone = null;
                SendTouchEvent(f, s);
            }
        }

        private void ProcessTouchUp(StoneController s) => _isTouchDown = false;

        private void ProcessTouchEnter(StoneController s)
        {
            if (!_isTouchDown || _selectedStone == null) return;
            _isTouchDown = false;
            
            var f = _selectedStone;
            _selectedStone = null;
            SendTouchEvent(f, s);
        }

        private void SendTouchEvent(StoneController lhv, StoneController rhv) => _game.SwapStones(lhv.Pos, rhv.Pos);
        
        void ICoreEventsReceiver.StoneCreated(in StoneCreateEvent e)
        {
            var stone = _stonePool.Take();
            stone.transform.position = new Vector3(e.pos.x, e.pos.y, 0);
            stone.Id = e.id;
            stone.Pos = e.pos;
            stone.Sprite = _stoneSprites[e.color];
            
            var td = stone.TouchDetector;
            td.OnTouchEvent += OnStoneTouchEvent;
            
            _stonesById[stone.Id] = stone;
            
            stone.gameObject.SetActive(true);
        }

        void ICoreEventsReceiver.StoneDestroyed(in StoneDestroyEvent e)
        {
            var stone = _stonesById[e.id];
                
            stone.Reset();
            _stonePool.Release(stone);
            _stonesById.Remove(e.id);
        }

        void ICoreEventsReceiver.StoneMoved(in StoneMoveEvent e)
        {
            var stone = _stonesById[e.id];
            
            stone.transform.position = new Vector3(e.pos.x, e.pos.y, 0);
            stone.Pos = e.pos;
        }

        void ICoreEventsReceiver.StoneChangeColor(in StoneChangeColorEvent e)
        {
            var stone = _stonesById[e.id];
            stone.Sprite = _stoneSprites[e.color];
        }
        
    }

}