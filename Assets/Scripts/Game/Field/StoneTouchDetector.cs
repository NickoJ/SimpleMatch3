using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Klyukay.SimpleMatch3.Game.Field
{

    public class StoneTouchDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
    {

        public StoneController Stone { get; set; }

        public event Action<TouchEvent> OnTouchEvent;

        private void OnDestroy() => Reset();
       
        public void Reset()
        {
            OnTouchEvent = null;
        }
        
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) => OnTouchEvent?.Invoke(new TouchEvent(TouchState.Down, Stone));
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) => OnTouchEvent?.Invoke(new TouchEvent(TouchState.Up, Stone));
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) => OnTouchEvent?.Invoke(new TouchEvent(TouchState.Enter, Stone));
    
    }
    
    public readonly struct TouchEvent
    {
        
        public readonly TouchState State;
        public readonly StoneController Controller;

        public TouchEvent(TouchState state, StoneController controller)
        {
            State = state;
            Controller = controller;
        }
        
    }

    public enum TouchState : byte { Down, Up, Enter }

}