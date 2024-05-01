using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimeScaleChanger : MonoBehaviour
{
    public void FadeTimeScale(float timeScale)
    {
        DOTween.To(() => Time.timeScale, t => Time.timeScale = t, timeScale, 0.25f);
    }
}
