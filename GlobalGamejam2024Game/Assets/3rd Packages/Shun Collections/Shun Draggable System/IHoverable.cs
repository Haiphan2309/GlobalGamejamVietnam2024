namespace Shun_Draggable_System
{
    public interface IHoverable
    {
        public bool IsHovering { get;}
        public void StartHover();

        public void EndHover();

    }
}