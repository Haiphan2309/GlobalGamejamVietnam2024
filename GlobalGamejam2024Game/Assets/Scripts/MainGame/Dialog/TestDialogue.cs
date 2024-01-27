using Unity;
using UnityEngine;

namespace MainGame.Dialog
{
	public class TestDialogue : MonoBehaviour
	{
		
		void Start()
    	{
        	DialogueManager.Instance.StartDialogue("Test", "This is a test dialogue");
    	}
	
		
    	
	}

}

