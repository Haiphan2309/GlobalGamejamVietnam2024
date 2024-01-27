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
        [SerializeField] GameObject vfx_WinGame;
        void Start()
        {
            gameOverPanel.DOColor(new(0, 0, 0, 0.4f), 1).OnComplete(() => 
            {
                float delay = 0f;
                if (vfx_WinGame != null)
                {
                    delay = 0.3f;
                    vfx_WinGame.SetActive(true);
                }
                gameOverText.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutBounce).SetDelay(delay);
                backButton.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutBounce).SetDelay(delay);
            });
        }
    }
}
