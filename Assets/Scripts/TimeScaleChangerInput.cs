using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Oculus.Interaction.OVR.Input;
using UnityEngine;

public class TimeScaleChangerInput : MonoBehaviour
{
    [SerializeField] private TimeScaleChanger timeScaleChanger;
    [SerializeField] private OVRPassthroughLayer passthroughLayer_Default;
    [SerializeField] private OVRPassthroughLayer passthroughLayer_SlowMo;
    [SerializeField] private OVRInput.RawButton ovrButton;

    void Update()
    {
        if (OVRInput.GetDown(ovrButton))
        {
            timeScaleChanger.FadeTimeScale(0.1f);
            DOTween.To(() => passthroughLayer_Default.textureOpacity, 
                o => passthroughLayer_Default.textureOpacity = o,
                0f, 0.25f);
            DOTween.To(() => passthroughLayer_SlowMo.textureOpacity, 
                o => passthroughLayer_SlowMo.textureOpacity = o,
                1f, 0.25f);
        }
        else if (OVRInput.GetUp(ovrButton))
        {
            timeScaleChanger.FadeTimeScale(1f);
            DOTween.To(() => passthroughLayer_Default.textureOpacity, 
                o => passthroughLayer_Default.textureOpacity = o,
                1f, 0.25f);
            DOTween.To(() => passthroughLayer_SlowMo.textureOpacity, 
                o => passthroughLayer_SlowMo.textureOpacity = o,
                0f, 0.25f);
        }
    }
}
