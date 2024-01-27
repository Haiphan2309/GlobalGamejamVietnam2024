
using UnityEngine;
using UnityUtilities;


public class DialogInputManager : SingletonMonoBehaviour<DialogInputManager>
{
    
    public bool GetSubmitPressed()
    {
        return Input.GetMouseButtonDown(0);
    }
    
    
    public bool RegisterSubmitPressed()
    {
        return Input.GetMouseButtonDown(0);
    }
}
