using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GDC.Enums;
using DG.Tweening;

namespace GDC.Home
{
    public class Point : MonoBehaviour
    {
        public PointType type;
        public float minTweenTime, maxTweenTime;
        public float minRadius, maxRadius;
        float[] direction = new float[2] {-1, 1};
        float tweenTime;
        float radius;
        public void Bound(Transform target)
        {
            this.tweenTime = Random.Range(this.minTweenTime, this.maxTweenTime);
            this.radius = Random.Range(this.minRadius, this.maxRadius);
            Vector2 pointAround = RandomOnUnitSphere(Vector2.zero, radius);
            Vector3 boundTarget = (Vector3)pointAround;
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            transform.DOScale(Vector3.one, tweenTime).SetEase(Ease.OutBack);
            transform.DOMove(boundTarget, tweenTime).SetEase(Ease.InOutSine)
            .OnComplete(() => 
            {
                transform.DOMove(target.position, 1)
                .SetDelay(Random.Range(0.25f, 0.75f))
                .OnComplete(() => gameObject.SetActive(false));
            });
        }
        Vector2 RandomOnUnitSphere(Vector2 _center, float _radius = 1.0f)
        {
            float randomX = direction[Random.Range(0, direction.Length)];
            float randomY = direction[Random.Range(0, direction.Length)];
            var theta = Random.value;
            return Camera.main.WorldToScreenPoint(new Vector2(
                (float)(_radius * Mathf.Cos(theta) * randomX + _center.x),
                (float)(_radius * Mathf.Sin(theta) * randomY + _center.y)
            ));
        }
    }
}
