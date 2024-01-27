using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace GDC.Common
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] Image gameOverPanel;
        [SerializeField] RectTransform gameOverText, backButton;
        void Start()
        {
            gameOverPanel.DOColor(new(0, 0, 0, 0.4f), 1).OnComplete(() => 
            {
                gameOverText.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutBounce);
                backButton.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutBounce);
            });
        }
    }
}
