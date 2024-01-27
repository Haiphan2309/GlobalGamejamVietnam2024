namespace Shun_Draggable_System
{
    public interface IDraggable
    {
        public bool IsDragging { get; }
        public bool StartDrag();
        public bool EndDrag();
        public void DisableDrag();
        public void EnableDrag();
    }
}