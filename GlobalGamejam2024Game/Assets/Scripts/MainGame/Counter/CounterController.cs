using UnityEngine;

namespace MainGame.Counter
{
    public class CounterController : MonoBehaviour
    {
        [SerializeField] private CounterView _counterView;
        
        public void SetCounter(int counter)
        {
            _counterView.SetCounter(counter);
        }
        
        
    }
}

