using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.OVR.Input;
using UnityEngine;

public class TimeScaleChangerInput : MonoBehaviour
{
    [SerializeField] private TimeScaleChanger timeScaleChanger;
    [SerializeField] private OVRInput.RawButton ovrButton;

    void Update()
    {
        if (OVRInput.GetDown(ovrButton))
        {
            timeScaleChanger.FadeTimeScale(0.1f);
        }
        else if (OVRInput.GetUp(ovrButton))
        {
            timeScaleChanger.FadeTimeScale(1f);
        }
    }
}
