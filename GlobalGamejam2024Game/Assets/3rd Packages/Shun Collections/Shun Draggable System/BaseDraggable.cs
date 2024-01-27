
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shun_Draggable_System
{
    [RequireComponent(typeof(Collider2D))]
    public class BaseDraggable : MonoBehaviour, IDraggable, IHoverable
    {
        protected BaseDraggableMouseInput MouseInput;
        public bool IsDestroyed { get; protected set; }
        public bool IsDraggable = true; 
        public bool IsDragging { get; private set; }

        public bool IsHoverable = true;
        public bool IsHovering { get; private set; }
        
        [SerializeField] protected bool ActivateOnValidate = false;

        
        public Action<BaseDraggable> OnDestroy { get; set; }
        public Action OnStartDrag { get; set; }
        public Action OnEndDrag { get; set; }
        public Action OnStartHover { get; set; }
        public Action OnEndHover { get; set; }
        public Action OnDisableInteractable { get; set; }
        public Action OnEnableInteractable { get; set; }
        
        public virtual bool StartDrag()
        {
            if (!IsDraggable) return false;
            IsDragging = true;
            
            OnStartDrag?.Invoke();
            return true;
        }

        public virtual bool EndDrag()
        {
            if (!IsDraggable) return false;
            IsDragging = false;
            
            OnEndDrag?.Invoke();
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
            
            OnStartHover?.Invoke();
        }

        public virtual void EndHover()
        {
            IsHovering = false;
            
            OnEndHover?.Invoke();
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

        public void SetMouseInput(BaseDraggableMouseInput mouseInput)
        {
            MouseInput = mouseInput;
        }
        
        public void RemoveMouseInput(BaseDraggableMouseInput mouseInput)
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
