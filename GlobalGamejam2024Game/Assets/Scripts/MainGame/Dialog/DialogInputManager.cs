
using UnityEngine;
using UnityEngine.InputSystem;
using UnityUtilities;

public class DialogInputManager : SingletonMonoBehaviour<DialogInputManager>
{
    private bool submitPressed = false;
    
    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }
    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }
    
    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }
}
