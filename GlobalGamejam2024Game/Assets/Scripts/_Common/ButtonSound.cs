using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AudioPlayer;
using GDC.Managers;

namespace GDC.Common
{
    public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        SoundID buttonHoverID = SoundID.SFX_HOVER_BUTTON;
        SoundID buttonClickID = SoundID.SFX_CLICK_BUTTON;
        public void OnPointerDown(PointerEventData eventData)
        {
            SoundManager.Instance.PlaySound(buttonClickID, 0.5f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SoundManager.Instance.PlaySound(buttonHoverID, 0.5f);
        }
    }
}
