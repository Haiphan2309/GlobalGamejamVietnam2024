using UnityEngine;
using DG.Tweening;

public class HappyHeartBeat : MonoBehaviour
{
    [SerializeField] private float _beatDuration = 0.5f;
    [SerializeField] private float _scaleMultiplier = 1.2f;

    private void Start()
    {
        // Start the heartbeat effect when the script is enabled
        StartHeartbeat();
    }

    private void Update()
    {
        // You can add other logic here if needed
    }

    private void StartHeartbeat()
    {
        // Use DOTween to create a heartbeat effect
        transform.DOScale(transform.localScale * _scaleMultiplier, _beatDuration / 2)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                transform.DOScale(transform.localScale / _scaleMultiplier, _beatDuration / 2)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(StartHeartbeat); // Recursive call for continuous heartbeat
            });
    }
}