using System.Collections.Generic;
using Shun_Draggable_System;
using Shun_Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shun_Draggable_System
{
    public class BaseDraggableMouseInput 
    {
        protected Vector3 MouseWorldPosition;
        protected RaycastHit2D[] MouseCastHits;
    
        [Header("Hover Objects")]
        protected List<IHoverable> LastHoverMouseInteractableGameObjects = new();
        public bool IsHoveringDraggable => LastHoverMouseInteractableGameObjects.Count != 0;

        [Header("Drag Objects")]
        protected Vector3 DraggableOffset;
        protected BaseDraggable Dragging;
        protected BaseDraggableRegion LastDraggableRegion;
        protected BaseDraggableHolder LastDraggableHolder;
        protected BaseClickable LastClickable;

        public bool IsDraggingDraggable
        {
            get;
            private set;
        }

    
        public virtual void UpdateMouseInput()
        {
            UpdateMousePosition();
            CastMouse();
            if(!IsDraggingDraggable) UpdateHoverObject();
            
            if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
            
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                StartDragDraggable();
            }

            if (Input.GetMouseButton(0))
            {
                DragObject();
            }

        }

        #region CAST

        protected virtual void UpdateMousePosition()
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseWorldPosition = new Vector3(worldMousePosition.x, worldMousePosition.y, 0);
        }

        protected virtual void CastMouse()
        {
            MouseCastHits = Physics2D.RaycastAll(MouseWorldPosition, Vector2.zero);
        }

        #endregion
    

        #region HOVER

        protected virtual void UpdateHoverObject()
        {
            var hoveringMouseInteractableGameObject = FindAllIMouseInteractableInMouseCast();

            var endHoverInteractableGameObjects = SetOperations.SetDifference(LastHoverMouseInteractableGameObjects, hoveringMouseInteractableGameObject);
            var startHoverInteractableGameObjects =  SetOperations.SetDifference(hoveringMouseInteractableGameObject, LastHoverMouseInteractableGameObjects);

            foreach (var interactable in endHoverInteractableGameObjects)
            {
                if (interactable.IsHovering) interactable.EndHover();
            }
            foreach (var interactable in startHoverInteractableGameObjects)
            {
                if (!interactable.IsHovering) interactable.StartHover();
            }

            LastHoverMouseInteractableGameObjects = hoveringMouseInteractableGameObject;
        }
    
    
        protected virtual IHoverable FindFirstIMouseInteractableInMouseCast()
        {
            foreach (var hit in MouseCastHits)
            {
                var clickable = hit.transform.gameObject.GetComponent<BaseClickable>();
                if (clickable != null && clickable.IsHoverable)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    return clickable;
                }
            
                var draggableGameObject = hit.transform.gameObject.GetComponent<BaseDraggable>();
                if (draggableGameObject != null && draggableGameObject.IsDraggable)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    return draggableGameObject;
                }
            }

            return null;
        }
    
        protected virtual List<IHoverable> FindAllIMouseInteractableInMouseCast()
        {
            List<IHoverable> mouseInteractableGameObjects = new(); 
            foreach (var hit in MouseCastHits)
            {
                var clickable = hit.transform.gameObject.GetComponent<BaseClickable>();
                if (clickable != null && clickable.IsHoverable)
                {
                    mouseInteractableGameObjects.Add(clickable);
                }
            
                var draggableGameObject = hit.transform.gameObject.GetComponent<BaseDraggable>();
                if (draggableGameObject != null && draggableGameObject.IsDraggable)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    mouseInteractableGameObjects.Add(draggableGameObject);
                }
            }

            return mouseInteractableGameObjects;
        }
    
        #endregion
    
    
        protected virtual TResult FindFirstInMouseCast<TResult>()
        {
            foreach (var hit in MouseCastHits)
            {
                var result = hit.transform.gameObject.GetComponent<TResult>();
                if (result != null)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    return result;
                }
            }

            //Debug.Log("Mouse cannot find "+ typeof(TResult));
            return default;
        }

    
        protected bool StartDragDraggable()
        {
            // Check for button first
            LastClickable = FindFirstInMouseCast<BaseClickable>();

            if (LastClickable != null && LastClickable.IsHoverable)
            {
                LastClickable.Select();
                return true;
            } 

            // Check for card game object second
            Dragging = FindFirstInMouseCast<BaseDraggable>();

            if (Dragging == null || !Dragging.IsDraggable || !DetachDraggableToHolder())
            {
                Dragging = null;
                return false;
            }
            
            // Successfully detach card
            DraggableOffset = Dragging.transform.position - MouseWorldPosition;
            IsDraggingDraggable = true;

            Dragging.StartDrag();
            Dragging.SetMouseInput(this);
            return true;
        
        }

        protected void DragObject()
        {
            if (!IsDraggingDraggable) return; 
        
            Dragging.transform.position = MouseWorldPosition + DraggableOffset;
        
        }

        private void EndDrag()
        {
            if (!IsDraggingDraggable) return;

            
            Dragging.EndDrag();
            if (Dragging != null) Dragging.RemoveMouseInput(this);
            AttachDraggableToHolder();

            Dragging = null;
            LastDraggableHolder = null;
            LastDraggableRegion = null;
            IsDraggingDraggable = false;

        }

        public void ForceEndDragAndDetachTemporary()
        {
            if (LastDraggableRegion != null) // remove the temporary in last region
            {
                LastDraggableRegion.RemoveTemporary(Dragging);
                
            }
            
            Dragging = null;
            LastDraggableHolder = null;
            LastDraggableRegion = null;
            IsDraggingDraggable = false;
        }
    
        protected virtual bool DetachDraggableToHolder()
        {
            if (Dragging.IsDestroyed) return false;
            
            // Check the card region base on card game object or card holder, to TakeOutTemporary
            LastDraggableRegion = FindFirstInMouseCast<BaseDraggableRegion>();
            if (LastDraggableRegion == null)
            {
                LastDraggableHolder = FindFirstInMouseCast<BaseDraggableHolder>();
                if (LastDraggableHolder == null)
                {
                    return true;
                }

                LastDraggableRegion = LastDraggableHolder.DraggableRegion;
            }
            else
            {
                LastDraggableHolder = LastDraggableRegion.FindDraggablePlaceHolder(Dragging);
            }

            // Having got the region and holder, take the card out temporary
            if ((!LastDraggableRegion.CheckCompatibleObject(Dragging) )||
            (LastDraggableRegion.CheckCompatibleObject(Dragging) 
             && LastDraggableRegion.TakeOutTemporary(Dragging, LastDraggableHolder))) return true;
        
            LastDraggableHolder = null;
            LastDraggableRegion = null;

            return false;

        }

        protected void AttachDraggableToHolder()
        {
            if (Dragging == null || Dragging.IsDestroyed) return;
            var dropRegion = FindFirstInMouseCast<BaseDraggableRegion>();
            var dropHolder = FindFirstInMouseCast<BaseDraggableHolder>();
        
            if (dropHolder == null)
            {
                if (dropRegion != null && dropRegion != LastDraggableRegion &&
                    dropRegion.TryAddDraggable(Dragging, dropHolder)) // Successfully add to the drop region
                {
                    if (LastDraggableHolder != null) // remove the temporary in last region
                    {
                        LastDraggableRegion.RemoveTemporary(Dragging);
                        return;
                    }
                }
            
                if (LastDraggableRegion != null) // Unsuccessfully add to drop region or it is the same region
                    LastDraggableRegion.ReAddTemporary(Dragging);
            }
            else
            {
                if (dropRegion == null) 
                    dropRegion = dropHolder.DraggableRegion;
                
                if (dropRegion == null) // No region to drop anyway
                {
                    if(LastDraggableRegion != null) LastDraggableRegion.ReAddTemporary(Dragging);
                }

                if (dropRegion.DraggableMiddleInsertionStyle == BaseDraggableRegion.MiddleInsertionStyle.Swap)
                {
                    var targetDraggable = dropHolder.Draggable;
                    if (targetDraggable != null && LastDraggableRegion != null && dropRegion.TakeOutTemporary(targetDraggable, dropHolder))
                    {
                        LastDraggableRegion.ReAddTemporary(targetDraggable);
                        dropRegion.ReAddTemporary(Dragging);
                        
                        return;
                    }
                    
                }
                
                if (!dropRegion.TryAddDraggable(Dragging, dropHolder))
                {
                    if(LastDraggableRegion != null) LastDraggableRegion.ReAddTemporary(Dragging);
                }
                
                if (LastDraggableHolder != null)
                {
                    LastDraggableRegion.RemoveTemporary(Dragging);
                }

            }

        }
    
    }
}
