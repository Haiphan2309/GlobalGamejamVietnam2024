using UnityEngine;

namespace Shun_Draggable_System
{
    public class MouseInputSystem : MonoBehaviour
    {
        private BaseDraggableMouseInput _mouseInput = new ();
        
        private void Awake()
        {
            
        }
        
        private void Update()
        {
            _mouseInput.UpdateMouseInput();
        }
        
        
        
        
    }
}