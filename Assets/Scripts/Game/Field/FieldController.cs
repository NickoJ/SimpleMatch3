using System.Collections.Generic;
using Klyukay.SimpleMatch3.Core;
using Klyukay.SimpleMatch3.Core.Events;
using Klyukay.SimpleMatch3.Game.Helpers;
using Klyukay.SimpleMatch3.Game.Settings;
using Unity.Mathematics;
using UnityEngine;

namespace Klyukay.SimpleMatch3.Game.Field
{

    public class FieldController : MonoBehaviour, ICoreEventsReceiver
    {

        [SerializeField] private Camera cam;

        private MonoPool<StoneController> _stonePool;
        private StoneSprites _stoneSprites;

        private Dictionary<int, StoneController> _stonesById = new Dictionary<int, StoneController>();
        
        public void Initialize(GameSettings settings)
        {
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

        void ICoreEventsReceiver.StoneCreated(in StoneCreateEvent e)
        {
            var stone = _stonePool.Take();
            stone.transform.position = new Vector3(e.pos.x, e.pos.y, 0);
            stone.SetData(e.id, e.pos, _stoneSprites[e.color]);
            
            _stonesById[stone.Id] = stone;
            
            stone.gameObject.SetActive(true);
        }

        void ICoreEventsReceiver.StoneDestroyed(int id)
        {
            throw new System.NotImplementedException();
        }

        void ICoreEventsReceiver.StoneMoved(int id, int2 pos)
        {
            throw new System.NotImplementedException();
        }
    }

}