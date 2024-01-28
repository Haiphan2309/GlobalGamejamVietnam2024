using UnityEngine;

namespace MainGame.Counter
{
    public class CounterController : MonoBehaviour
    {
        [SerializeField] private CounterView _counterView;
        [SerializeField] private int _counter = 6;
        
        private void Awake()
        {
            _counterView.SetCounter(_counter);
        }

        public void SetCounter(int counter)
        {
            _counter = counter;
            _counterView.SetCounter(_counter);
        }

        public void IncreaseCounter()
        {
            _counter++;
            _counterView.SetCounter(_counter);
        }

        public void DecreaseCounter()
        {
            _counter--;
            _counterView.SetCounter(_counter);
            
            if (_counter < 0)
            {
                // TODO : End Game
                
                
            }
        }
    
    
    }
}

