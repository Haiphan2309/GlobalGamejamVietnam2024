
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shun_Card_System
{
    [RequireComponent(typeof(Collider2D))]
    public class BaseDraggableObject : MonoBehaviour, IMouseDraggable, IMouseHoverable
    {
        public Action<BaseDraggableObject> OnDestroy { get; set; }
        protected BaseDraggableObjectMouseInput MouseInput;
        public bool IsDestroyed { get; protected set; }
        public bool IsDraggable = true; 
        public bool IsDragging { get; private set; }

        public bool IsHoverable = true;
        public bool IsHovering { get; private set; }
        
        [SerializeField] protected bool ActivateOnValidate = false;

        
        public virtual bool StartDrag()
        {
            if (!IsDraggable) return false;
            IsDragging = true;
            return true;
        }

        public virtual bool EndDrag()
        {
            if (!IsDraggable) return false;
            IsDragging = false;
            return true;
        }
        
        private void OnValidate()
        {
            if (ActivateOnValidate) ValidateInformation();
        }
        
        protected virtual void ValidateInformation()
        {
            
        }
        
        public virtual void StartHover()
        {
            IsHovering = true;
        }

        public virtual void EndHover()
        {
            IsHovering = false;
        }
        
        public virtual void DisableDrag()
        {
            if (!IsDraggable) return;
            IsDraggable = false;
            if (IsHovering) EndHover();
        }
        
        public virtual void EnableDrag()
        {
            if (IsDraggable) return;
            IsDraggable = true;
        }

        public void SetMouseInput(BaseDraggableObjectMouseInput mouseInput)
        {
            MouseInput = mouseInput;
        }
        
        public void RemoveMouseInput(BaseDraggableObjectMouseInput mouseInput)
        {
            MouseInput = null;    
        }

        public void Destroy()
        {
            IsDestroyed = true;
            OnDestroy?.Invoke(this);
        }
    }
}
