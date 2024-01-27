using Shun_Unity_Editor;
using UnityEngine;

namespace Shun_Draggable_System
{
    /// <summary>
    /// This class is the card place holder of a card object in card place region.
    /// This can be used to move, animations,...
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class BaseDraggableHolder : MonoBehaviour
    {
        [HideInInspector] public BaseDraggableRegion DraggableRegion;
        [HideInInspector] public int IndexInRegion;
        [ShowImmutable] public BaseDraggable Draggable;

        
        public void InitializeRegion(BaseDraggableRegion draggableRegion, int indexInRegion)
        {
            DraggableRegion = draggableRegion;
            IndexInRegion = indexInRegion;
        }
        
        public void AttachDraggableGameObject(BaseDraggable draggable)
        {
            if (draggable == null) return;
            
            Draggable = draggable;
            Draggable.transform.SetParent(transform, true);
            
            Draggable.DisableDrag();
            AttachDraggableVisual();
        }

        public BaseDraggable DetachDraggableGameObject()
        {
            if (Draggable == null) return null;
            
            BaseDraggable detachedDraggable = Draggable;
            
            detachedDraggable.transform.SetParent(DraggableRegion.transform.parent, true);

            DetachDraggableVisual();
            Draggable = null;
            
            return detachedDraggable;
        }

        protected virtual void DetachDraggableVisual()
        {
            
        }

        protected virtual void AttachDraggableVisual()
        {
            Draggable.transform.localPosition = Vector3.zero;
            Draggable.EnableDrag();
        }
        
        
    }
}