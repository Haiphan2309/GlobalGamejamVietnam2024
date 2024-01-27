using Shun_Unity_Editor;
using UnityEngine;

namespace Shun_Card_System
{
    /// <summary>
    /// This class is the card place holder of a card object in card place region.
    /// This can be used to move, animations,...
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class BaseDraggableObjectHolder : MonoBehaviour
    {
        [HideInInspector] public BaseDraggableObjectRegion DraggableObjectRegion;
        [HideInInspector] public int IndexInRegion;
        [ShowImmutable] public BaseDraggableObject DraggableObject;

        
        public void InitializeRegion(BaseDraggableObjectRegion draggableObjectRegion, int indexInRegion)
        {
            DraggableObjectRegion = draggableObjectRegion;
            IndexInRegion = indexInRegion;
        }
        
        public void AttachCardGameObject(BaseDraggableObject draggableObject)
        {
            if (draggableObject == null) return;
            
            DraggableObject = draggableObject;
            DraggableObject.transform.SetParent(transform, true);
            
            DraggableObject.DisableDrag();
            AttachCardVisual();
        }

        public BaseDraggableObject DetachCardGameObject()
        {
            if (DraggableObject == null) return null;
            
            BaseDraggableObject detachedDraggable = DraggableObject;
            
            detachedDraggable.transform.SetParent(DraggableObjectRegion.transform.parent, true);

            DetachCardVisual();
            DraggableObject = null;
            
            return detachedDraggable;
        }

        protected virtual void DetachCardVisual()
        {
            
        }

        protected virtual void AttachCardVisual()
        {
            DraggableObject.transform.localPosition = Vector3.zero;
            DraggableObject.EnableDrag();
        }
        
        
    }
}