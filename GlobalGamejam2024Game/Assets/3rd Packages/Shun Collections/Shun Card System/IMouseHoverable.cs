namespace Shun_Card_System
{
    public interface IMouseHoverable
    {
        public bool IsHovering { get;}
        public void StartHover();

        public void EndHover();

    }
}