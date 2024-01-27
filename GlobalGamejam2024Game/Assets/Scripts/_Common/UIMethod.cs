using System.Collections;
using System.Collections.Generic;
using GDC.Managers;
using AudioPlayer;
using UnityEngine;
using DG.Tweening;

namespace GDC.Common
{
    public class UIMethod : MonoBehaviour
    {
        public void PointerEnterButton()
        {
            // SoundManager.Instance.PlaySound(SoundID.SFX_HOVER_BUTTON);
            GetComponent<RectTransform>().DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.25f);
        }
        public void PointerExitButton()
        {
            // SoundManager.Instance.PlaySound(SoundID.SFX_HOVER_BUTTON);
            GetComponent<RectTransform>().DOScale(Vector3.one, 0.25f);
        }
        public void StartGame()
        {
            print("START");
        }
        public void BackToMainMenu()
        {
            print("BACK");
        }
    }
}
