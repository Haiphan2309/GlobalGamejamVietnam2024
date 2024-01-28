using System.Collections;
using System.Collections.Generic;
using GDC.Managers;
using AudioPlayer;
using UnityEngine;
using DG.Tweening;
using GDC.Enums;

namespace GDC.Common
{
    public class UIMethod : MonoBehaviour
    {
        // [SerializeField] RectTransform creditPanel;
        public void PointerEnterButton()
        {
            SoundManager.Instance.PlaySound(SoundID.SFX_HOVER_BUTTON);
            GetComponent<RectTransform>().DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.25f);
        }
        public void PointerExitButton()
        {
            GetComponent<RectTransform>().DOScale(Vector3.one, 0.25f);
        }
        public void PointerClickButton()
        {
            SoundManager.Instance.PlaySound(SoundID.SFX_CLICK_BUTTON);
        }
        public void StartGame()
        {
            GameManager.Instance.LoadSceneManually(SceneType.LEVEL_MENU, TransitionType.LEFT);
        }
        public void BackToMainMenu()
        {
            GameManager.Instance.LoadSceneManually(SceneType.MAIN_MENU, TransitionType.LEFT);
        }
        public void EnterLevel(int level)
        {
            if (level == 1)
                GameManager.Instance.LoadSceneManually(SceneType.LEVEL_1, TransitionType.LEFT);
            else if (level == 2)
                GameManager.Instance.LoadSceneManually(SceneType.LEVEL_2, TransitionType.LEFT);
        }
        public void CreditButton(RectTransform creditPanel)
        {
            creditPanel.DOLocalMoveX(0, 0.5f);
        }
        public void CreditButtonExit(RectTransform creditPanel)
        {
            creditPanel.DOLocalMoveX(-2000, 0.5f).OnComplete(() => 
                creditPanel.transform.localPosition = new Vector3(2000, 0, 0)
            );
        }
    }
}
