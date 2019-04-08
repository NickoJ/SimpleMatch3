using Unity.Mathematics;
using UnityEngine;

namespace Klyukay.SimpleMatch3.Game.Field
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(StoneTouchDetector))]
    public class StoneController : MonoBehaviour
    {

        private SpriteRenderer _renderer;

        public StoneTouchDetector TouchDetector { get; private set; }

        public int Id { get; set; }
        public int2 Pos { get; set; }

        public Sprite Sprite
        {
            get => _renderer.sprite;
            set => _renderer.sprite = value;
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            TouchDetector = GetComponent<StoneTouchDetector>();
            TouchDetector.Stone = this;
        }
        
        public void Reset()
        {
            _renderer.sprite = null;
            TouchDetector.Reset();
            gameObject.SetActive(false);
        }

    }
    
}