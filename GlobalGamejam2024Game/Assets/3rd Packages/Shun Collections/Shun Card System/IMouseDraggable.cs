namespace Shun_Card_System
{
    public interface IMouseDraggable
    {
        public bool IsDragging { get; }
        public bool StartDrag();
        public bool EndDrag();
        public void DisableDrag();
        public void EnableDrag();
    }
}