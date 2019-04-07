using Unity.Mathematics;
using UnityEngine;

namespace Klyukay.SimpleMatch3.Game.Field
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class StoneController : MonoBehaviour
    {

        private SpriteRenderer _renderer;
        
        public int Id { get; private set; }
        public int2 Pos { get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        
        public void Reset()
        {
            if (_renderer != null) _renderer.sprite = null;
            gameObject.SetActive(false);
        }

        public void SetData(int id, int2 pos, Sprite sprite)
        {
            Id = id;
            Pos = pos;
            _renderer.sprite = sprite;
        }
        
    }
    
}