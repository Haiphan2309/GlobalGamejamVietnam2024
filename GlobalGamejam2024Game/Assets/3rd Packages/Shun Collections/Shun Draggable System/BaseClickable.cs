using System;
using UnityEngine;

namespace Shun_Draggable_System
{
    [RequireComponent(typeof(Collider2D))]
    public class BaseClickable : MonoBehaviour, IHoverable, IClickable
    {
        
        [SerializeField]
        private bool _interactable;
        public bool IsHoverable { get => _interactable; protected set => _interactable = value; }
        public bool IsHovering { get; protected set; }
        public bool IsDestroyed { get; protected set; }
        
        public Action<BaseClickable> OnDestroy { get; set; }
        public Action OnSelect { get; set; }
        public Action OnDeselect { get; set; }
        public Action OnStartHover { get; set; }
        public Action OnEndHover { get; set; }
        public Action OnDisableInteractable { get; set; }
        public Action OnEnableInteractable { get; set; }
        
        
        
        public virtual void Select()
        {
            OnSelect?.Invoke();
        }

        public virtual void Deselect()
        {
            OnDeselect?.Invoke();
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

        public virtual void DisableInteractable()
        {
            if (!IsHoverable) return;
            IsHoverable = false;
            if (IsHovering) EndHover();
            
            OnDisableInteractable?.Invoke();
        }
        
        public virtual void EnableInteractable()
        {
            if (IsHoverable) return;
            IsHoverable = true;
            
            OnEnableInteractable?.Invoke();
        }

        public void Destroy()
        {
            IsDestroyed = true;
            OnDestroy?.Invoke(this);
        }
        
    }
}