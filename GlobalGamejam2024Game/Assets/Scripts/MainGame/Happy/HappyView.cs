using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HappyView : MonoBehaviour
{
    
    [SerializeField] private Slider _slider;
    
    [SerializeField] private float _moveAnimationSpeed = 0.5f;
    
    private float _currentPercent;
    private float _maxPercent;

    // Callback to be invoked when the timer completes
    public Action OnFillComplete;

    
    public void Initialize(float maxPercent, float currentPercent = 0, Action onTimerComplete = null)
    {
        _maxPercent = maxPercent;
        _currentPercent = currentPercent < 0 ? _currentPercent : Mathf.Min(currentPercent,maxPercent);
        OnFillComplete = onTimerComplete;
        
        MoveSlider(_currentPercent, _maxPercent);
    }
        
    
    
    public void AddValue(float timeToAdd)
    {
        
        MoveSlider(_currentPercent, _currentPercent+timeToAdd);
    }

    private void MoveSlider(float startTime, float endTime)
    {
        
        _slider.value = _currentPercent / _maxPercent;

        // Use OnComplete callback to trigger actions when the animation completes
        _slider.DOValue(endTime / _maxPercent,( (endTime - startTime)/(endTime*_moveAnimationSpeed)))
            .OnComplete(() =>
            {
                // Optionally, you can perform additional actions here
                OnFillComplete?.Invoke();
                
                
                _currentPercent = endTime;
            });
    }
    
    
    
}
