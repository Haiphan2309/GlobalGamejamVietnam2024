using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AudioPlayer
{
    [Serializable]
    public enum SoundID
    {
        NONE = -1,
        _____COMMON_____ = 0,
        CLICK_CARD,
        CLICK_OBJECT,
        HOVER_CARD,
        SFX_TRANSITION_IN,
        SFX_TRANSITION_OUT,
        SFX_LIGHT_TRANSITION,
        SFX_HOVER_BUTTON,
        SFX_CLICK_BUTTON,
        SFX_HAPPY,
        SFX_BORING,
    }
}