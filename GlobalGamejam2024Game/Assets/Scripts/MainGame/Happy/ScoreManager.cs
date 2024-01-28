using System;
using GDC.Common;
using MainGame.Counter;
using UnityEngine;
using UnityUtilities;

namespace MainGame.Happy
{
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
    {
        
        [SerializeField] private int _cardUseCounter = 6;
    
        [Header("View")]
        [SerializeField] private HappyView _happyView;
        [SerializeField] private CounterController _counterController;
        
        [SerializeField] private GameOver _winGameOver;
        [SerializeField] private GameOver _loseGameOver;
        
        private int _counter;
        private float _happyValue = 0;
        

        public Action OnCounterEnd { get; set; }
        public Action OnHappyFill { get; set; }

        private void Awake()
        {
            InitializeHappy(0);
            InitializeCounter(_cardUseCounter);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IncreaseHappy(0.2f);
            }
        }

        public void InitializeHappy(float initialHappyValue, float maxHappyValue = 1)
        {
            _happyValue = initialHappyValue;

            _happyView.Initialize(maxHappyValue,initialHappyValue);
            
        }
        
        public void IncreaseHappy(float happyValue)
        {
            _happyValue += happyValue;
            _happyView.AddValue(happyValue);

            if (_happyValue >= 1)
            {
                
                _winGameOver.gameObject.SetActive(true);
                
                OnHappyFill?.Invoke();
                
            }
        }
        
        
        
        public void InitializeCounter(int counter, Action onCounterEnd = null)
        {
            _counter = counter;
            _counterController.SetCounter(counter);
            OnCounterEnd = onCounterEnd;
        }
        

        public void IncreaseCounter()
        {
            _counter++;
            _counterController.SetCounter(_counter);
        }

        public void DecreaseCounter()
        {
            _counter--;
            _counterController.SetCounter(_counter);
            
            if (_counter < 0)
            {
                // TODO : End Game
                
                _loseGameOver.gameObject.SetActive(true);
                
                
                OnCounterEnd?.Invoke();
            }
        }
        

        
        
        
        
    }
}